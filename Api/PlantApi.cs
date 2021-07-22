using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;


namespace gardenit_web.Api
{
    public class PlantApi : IApi
    {
        private readonly IRestClient _client;

        public PlantApi(IRestClient client, IOptions<ApiOptions> optionsAccessor) {
            _client = client;
            _client.BaseUrl = new Uri(optionsAccessor.Value.Url);
            _client.UseNewtonsoftJson();
        }
        
        public void Post(string endpoint, object body) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            restRequest.AddJsonBody(body);
            var response = _client.Post(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

        public async Task PostAsync(string endpoint, object body) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            restRequest.AddJsonBody(body);
            var response = await _client.ExecutePostAsync(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

        public void Put(string endpoint, object body) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            restRequest.AddJsonBody(body);
            var response = _client.Put(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

        public T Get<T>(string endpoint) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            var response = _client.Get<T>(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return response.Data;
        }

        public void Delete(string endpoint) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            var response = _client.Delete(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

    }
}