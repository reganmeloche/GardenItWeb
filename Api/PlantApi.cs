using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;


namespace gardenit_web.Api
{
    public class PlantApi : IApi
    {
        private readonly IRestClient _client;
        //private readonly 
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEncryptor _encryptor;
        private readonly string _encryptionKey;
        

        public PlantApi(
            IRestClient client, 
            IOptions<ApiOptions> optionsAccessor, 
            UserManager<IdentityUser> userManager, 
            IHttpContextAccessor httpContextAccessor,
            IEncryptor encryptor
        ) {
            _client = client;
            _client.BaseUrl = new Uri(optionsAccessor.Value.Url);
            _client.UseNewtonsoftJson();

            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _encryptor = encryptor;
            _encryptionKey = optionsAccessor.Value.EncryptionKey;
        }
        
        public void Post(string endpoint, object body) {
            var (encryptedUserId, iv) = GetEncryptedUserIdAndIV();

            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            restRequest.AddHeader("UserId", encryptedUserId);
            restRequest.AddHeader("iv", iv);

            restRequest.AddJsonBody(body);
            var response = _client.Post(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

        public async Task PostAsync(string endpoint, object body) {
            var (encryptedUserId, iv) = GetEncryptedUserIdAndIV();

            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            restRequest.AddHeader("UserId", encryptedUserId);
            restRequest.AddHeader("iv", iv);

            restRequest.AddJsonBody(body);
            var response = await _client.ExecutePostAsync(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

        public void Put(string endpoint, object body) {
            var (encryptedUserId, iv) = GetEncryptedUserIdAndIV();

            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            restRequest.AddHeader("UserId", encryptedUserId);
            restRequest.AddHeader("iv", iv);

            restRequest.AddJsonBody(body);
            var response = _client.Put(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

        public T Get<T>(string endpoint) {
            var (encryptedUserId, iv) = GetEncryptedUserIdAndIV();

            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            restRequest.AddHeader("UserId", encryptedUserId);
            restRequest.AddHeader("iv", iv);

            var response = _client.Get<T>(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return response.Data;
        }

        public void Delete(string endpoint) {
            var (encryptedUserId, iv) = GetEncryptedUserIdAndIV();

            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            restRequest.AddHeader("UserId", encryptedUserId);
            restRequest.AddHeader("iv", iv);

            var response = _client.Delete(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new ApiException(response.ErrorMessage);
            }
            return;
        }

        private (string encryptedUserId, string iv) GetEncryptedUserIdAndIV() {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _encryptor.Encrypt(_encryptionKey, userId);
        }
        

    }
}