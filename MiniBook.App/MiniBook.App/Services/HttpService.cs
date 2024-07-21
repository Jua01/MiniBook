using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiniBook.App.Services
{
    public class HttpService
    {
        public async Task SendAsync(string url, HttpMethod method, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (content != null)
            {
                request.Content = content;
            }

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    //read body as json

                }
            }
        }

        private HttpClient DefaultHttpClient()
        {
            return new HttpClient();
        }
    }
}
