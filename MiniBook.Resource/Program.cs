using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#if DEBUG
builder.WebHost.UseUrls("https://*:14025");
#endif
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("Api1Policy", builder =>
        {
            builder.RequireClaim("scope", "api");
        });
        options.AddPolicy("Api2Policy", builder =>
        {
            builder.RequireClaim("scope", "api2");
        });
    }
    );
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    //.AddJwtBearer(o =>
    //{
    //    o.Authority = configuration["Identity:Authority"];
    //    o.TokenValidationParameters.ValidateAudience = false;
    //});
    .AddIdentityServerAuthentication(
    options =>
    {
        options.Authority = configuration["Identity:Authority"];
        options.RequireHttpsMetadata = false;
        options.ApiSecret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";

    });

//builder.Services.AddScoped<DataContext>(
//    provider => new DataContext(configuration["Data:ConnectionString"], 
//                                configuration["Data:DbName"]));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
