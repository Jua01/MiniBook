using MiniBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiniBook.Services
{
    public class HttpService
    {
        public async Task<T> SendAsync<T>(string url, HttpMethod method, HttpContent content = null)
        {
            
            var request = new HttpRequestMessage(method, url);

            if (content != null)
            {
                request.Content = content;
            }

            //if (AppContext.Current.Token?.IsExpired()==false)
            //{
            //    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppContext.Current.Token.AccessToken);
            //}
            

            using (var client = DefaultHttpClient())
            {

                var response = await client.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();
                
                //read body as json
                return JsonConvert.DeserializeObject<T>(body);
            }
        }

        

        private HttpClient DefaultHttpClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
            return new HttpClient(clientHandler);

            //return new HttpClient();
        }
        internal Task<ApiResponse<T>> PostApiAsync<T>(string url, object body)
        {
            return PostAsync<ApiResponse<T>>(url, body);
        }
        internal Task<T> PostAsync<T>(string url, object body)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            return SendAsync<T>(url, HttpMethod.Post, content);
        }

        internal Task<T> PostAsync<T>(string url, Dictionary<string, string> form)
        {
            var content = new FormUrlEncodedContent(form);

            return SendAsync<T>(url, HttpMethod.Post, content);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            return await SendAsync<T>(url, HttpMethod.Get);
        }

        // Kiem tra token
        public async Task<T> GetAsync<T>(string url, string token)
        {
            return await SendTestAsync<T>(url, HttpMethod.Get, token);
        }


        public async Task<T> SendTestAsync<T>(string url, HttpMethod method, string token)
        {

            var request = new HttpRequestMessage(method, url);

            //if (AppContext.Current.Token?.IsExpired()==false)
            //{
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            //}
           
            using (var client = DefaultHttpClient())
            {

                var response = await client.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();

                //read body as json
                return JsonConvert.DeserializeObject<T>(body);
            }
        }
    }
}
