using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Localization;
using MiniBook.Data;
using MiniBook.Identity.Configuration;
using MiniBook.Identity.Extensions;
using MiniBook.Identity.Models;
using System.Globalization;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
#if DEBUG
builder.WebHost.UseUrls("https://*:7139/", "http://*:5105/");
#endif
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.WriteIndented = true;
});


//builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
//.AddJwtBearer(o =>
//{
//    o.Authority = "https://localhost:5001";
//    o.TokenValidationParameters.ValidateAudience = false;
//    o.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
//});
//.AddIdentityServerAuthentication(options =>
//{
//    options.Authority = "https://localhost:5001";
//    options.RequireHttpsMetadata = false;
//});

builder.Services.AddResourceData(builder.Configuration["Data:ConnectionString"],
                                    builder.Configuration["Data:DbName"]);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryPersistedGrants()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryApiScopes(Config.GetApiScope())
    .AddAspNetIdentity<User>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMvc();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = new[] {new CultureInfo("en"), new CultureInfo("vi")},
    SupportedUICultures = new[] { new CultureInfo("en"), new CultureInfo("vi") },
    RequestCultureProviders = new IRequestCultureProvider[] { 
        new QueryStringRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider(),
    },
});



app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
//app.Urls.Add("http://*:44328");
app.UseIdentityServer();
app.MapGet("/", () => "Hello World!");
app.Run();
