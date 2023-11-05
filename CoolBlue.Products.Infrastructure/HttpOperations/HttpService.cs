using CoolBlue.Products.Application.Common;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace CoolBlue.Products.Infrastructure.HttpOperations
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;


        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> CallAsync<TResponse>(HttpMethod httpMethod, string baseAddress, string endpoint)
        {
            //TODO: add try catch
            var request = new HttpRequestMessage(httpMethod, $"{baseAddress}/{endpoint}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);

            return Deserialize<TResponse>(response);
        }

        public async Task<TResponse> CallAsync<TRequest, TResponse>(HttpMethod httpMethod, string baseAddress, string endpoint, TRequest content)
        {
            var request = new HttpRequestMessage(httpMethod, $"{baseAddress}/{endpoint}")
            {
                Content = new StringContent(Serialize(content), Encoding.UTF8, "application/json")
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);

            Console.WriteLine($"statusCode => {response.StatusCode}");
            Console.WriteLine($"contest => {response.Content.ReadAsStringAsync().Result}");

            return Deserialize<TResponse>(response);
        }

        private TResponse Deserialize<TResponse>(HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;

            return string.IsNullOrEmpty(data) ? default : JsonConvert.DeserializeObject<TResponse>(data);
        }

        private string Serialize<TRequest>(TRequest request)
        {
            return JsonConvert.SerializeObject(request);
        }
    }
}
