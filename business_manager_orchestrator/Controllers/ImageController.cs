using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using System.Text.Json;
using System;
using business_manager_orchestrator.Clients;
using System.Text;
using business_manager_common_library;
using Microsoft.AspNetCore.Http;

namespace business_manager_orchestrator.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly string apiUrl;
        private readonly AuthClient authClient;

        private readonly IHttpClientFactory _httpClientFactory;
        public ImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            authClient = new AuthClient();
            apiUrl = Environment.GetEnvironmentVariable("apiUrl");
        }

        [HttpPost]
        public async Task<IActionResult> PostImage(ImageModel imageModel)
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            string jsonString = JsonSerializer.Serialize(imageModel);
            
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await apiClient.PostAsync(apiUrl + "/image", stringContent);
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                error = tokenResponse.Error,
                errorDescr = tokenResponse.ErrorDescription,
                message = content,
                status = response.StatusCode,
                data = response.Content.ReadAsStringAsync()
            });
        }

        [HttpPost("business/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> PostBusinessImage(long id, IFormFile image)
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            MultipartFormDataContent formData = new MultipartFormDataContent();
            //formData.Add(image);

            var response = await apiClient.PostAsync(apiUrl + "/image", formData);
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                error = tokenResponse.Error,
                errorDescr = tokenResponse.ErrorDescription,
                message = content,
                status = response.StatusCode,
                data = response.Content.ReadAsStringAsync()
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync(apiUrl + "/image/" + id);
            var content = await response.Content.ReadAsStringAsync();
            //UserAccountModel user = JsonSerializer.Deserialize<UserAccountModel>(content);

            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                error = tokenResponse.Error,
                errorDescr = tokenResponse.ErrorDescription,
                message = content,
                status = response.StatusCode,
                data = content
            });
        }
    }
}