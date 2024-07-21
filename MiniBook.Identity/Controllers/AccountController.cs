using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniBook.Identity.Models;
using MiniBook.Identity.ViewModels;
using MiniBook.Server.Shared;
using IdentityModel;
using MiniBook.Data.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MiniBook.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<User> userManager;
        public AccountController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet("registerFake")]
        public async Task<IActionResult> RegisterFakeData()
        {
            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetStringAsync("https://randomuser.me/api/?results=100&nat=gb,us&inc=gender,name,email,picture");

                var results = JsonConvert.DeserializeObject<JObject>(response)!.Value<JArray>("results")!;

                string UpFirst(string input)
                {
                    return char.ToUpper(input[0]) + input.Substring(1);
                }

                foreach (var randUser in results)
                {
                    var gender = UpFirst(randUser.Value<string>("gender")!);
                    var first = UpFirst(randUser.SelectToken("name.first")!.Value<string>()!);
                    var last = UpFirst(randUser.SelectToken("name.last")!.Value<string>()!);
                    var email = randUser.Value<string>("email");
                    var picture = randUser.SelectToken("picture.large")!.Value<string>();

                    var model = new RegisterViewModel()
                    {
                        Email = email,
                        FirstName = first,
                        LastName = last,
                        Gender = gender,
                        Image = picture,
                        Password = "String@123"
                    };

                    await Register(model);
                }

            }
            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return this.ErrorResult(ErrorCode.REGISTER_REQUIRED_EMAIL);
            }
            var user = new User() { Email = model.Email, UserName = model.Email };

            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.GivenName,
                ClaimValue = model.FirstName,
            });

            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.FamilyName,
                ClaimValue = model.LastName,
            });
            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.Gender,
                ClaimValue = model.Gender.ToString(),
            });
            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.BirthDate,
                ClaimValue = model.DoB.ToString("yyyy-MM-dd"),
            });
            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.Picture,
                ClaimValue = model.Image
            });

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await HttpContext.RequestServices.GetRequiredService<UserRepository>()
                    .CreateAsync(user.Id, model.FirstName + " " + model.LastName, model.Gender, model.Image);

                return this.OkResult();
            }
            else
            {
                if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
                {
                    return this.ErrorResult(ErrorCode.REGISTER_DUPLICATE_USER_NAME);
                }
                return this.ErrorResult(ErrorCode.BAD_REQUEST);
            };
        }

        [HttpPost("logout")]
        //[Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.OkResult();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync()
        {
            var url = "https://localhost:7139/connect/token";
            var method = HttpMethod.Post;

            //var response = await httpService.PostAsync<TokenResponse>(url, );
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "client_id","client"},
                { "client_secret","secret" },
                { "grant_type","password" },
                { "scope","api openid profile email"},
                { "username","khanh@gmail.com"},
                { "password","String@123"},
            });

            var request = new HttpRequestMessage(method, new System.Uri(url));
            //request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppContext.Current.Token.AccessToken);
            if (request == null) 
            {
                return this.OkResult(request+"Tại đây");
            }
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)

            using (var client = new HttpClient(clientHandler))
            {

                var response = await client.SendAsync(request);


                var body = await response.Content.ReadAsStringAsync();
                //read body as json
               return this.OkResult(body);
            }
            

            //    if (!string.IsNullOrEmpty(response.Error))
            //    {
            //        switch (response.Error)
            //        {
            //            case "invalid_grant":
            //                return false;
            //            default:
            //                return false;
            //        }
            //    }




            //    return true;
            //}
            //    return false;
        }
}


}
