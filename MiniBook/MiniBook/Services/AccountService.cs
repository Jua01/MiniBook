using MiniBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MiniBook.Services
{
    public class AccountService
    {
        private HttpService httpService { get; }
        public AccountService(HttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            //var url = Configuration.ID_HOST + "/connect/token";
            var url = Configuration.ID_HOST + "/users/authenticate";

            var response = await httpService.PostAsync<object>(url, new
            {
                //{ "client_id","client"},
                //{ "client_secret","secret" },
                //{ "grant_type","password" },
                //{ "scope","api openid profile email"},
               email,
               password,
            });
            var user = JsonConvert.SerializeObject(response);
            var test1 = JsonConvert.DeserializeObject<Dictionary<string, string>>(user);
            var token = "";

            foreach(var item in test1)
            {
                if(item.Key == "token")
                {
                 token = item.Value;
                 
                }

            }
            var resp = await GetUserAsync(token);



            //if (!string.IsNullOrEmpty(response.Error))
            //{
            //    switch(response.Error)
            //    {
            //        case "invalid_grant":
            //            return false;
            //        default:
            //            return false;
            //    }
            //}

            //if (!string.IsNullOrEmpty(response.AccessToken))
            //{
            //    //response.ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
            //    //await Xamarin.Essentials.SecureStorage.SetAsync(nameof(response.AccessToken), response.AccessToken);
            //    //await Xamarin.Essentials.SecureStorage.SetAsync(nameof(response.ExpiresIn), response.ExpiresIn.ToString());

            //    await SecureStorage.SetAsync("Token", JsonConvert.SerializeObject(response,Formatting.None));
            //    AppContext.Current.Token = response;

            //    //AppContext.Current.Profile = await GetProfileAsync();

            //    return true;
            //}
            return true;
        }

        public async Task<bool> RestoreAsync()
        {
          var token = await SecureStorage.GetAsync("Token");
            if(token != null)
            {
                AppContext.Current.Token = JsonConvert.DeserializeObject<TokenResponse>(token);

                if (AppContext.Current.Token.IsExpired())
                {
                    AppContext.Current.Token = null;
                    return false;
                }
                //AppContext.Current.Profile = await GetProfileAsync();

                return true;
            }
            return false;
        }
        
        public Task<ApiResponse<object>> RegisterAsync(User user, string password)
        {
            var url = Configuration.ID_HOST + "/api/account";
            //user.DoB = DateTime.Parse("2001-05-13");
            return httpService.PostApiAsync<object>(url, new
            {
                user.FirstName,
                user.LastName,
                user.Gender,
                user.Email,
                user.DoB,
                password
            });

        }

        public Task<User> GetProfileAsync()
        {
            var url = Configuration.ID_HOST + "/connect/userinfo";

            return httpService.GetAsync<User>(url);
        }

        public Task<object> GetUserAsync(string token)
        {
            var url = Configuration.ID_HOST + "/users";

            return httpService.GetAsync<object>(url, token);
        }



        //public Task<User> LogoutAsync()
        //{
        //    var url = Configuration.ID_HOST + "/connect/logout";

        //    return httpService.GetAsync();
        //}
    }
}
