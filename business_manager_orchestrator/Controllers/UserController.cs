using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using System.Text.Json;
using System;
using business_manager_orchestrator.Clients;
using System.Text;
using business_manager_common_library;
using Microsoft.AspNetCore.Authorization;

namespace business_manager_orchestrator.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly string apiUrl;
        private readonly AuthClient authClient;

        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            authClient = new AuthClient();
            apiUrl = Environment.GetEnvironmentVariable("apiUrl");
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserAccountModel userAccountModel)
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            string jsonString = JsonSerializer.Serialize(userAccountModel);
            
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await apiClient.PostAsync(apiUrl + "/user", stringContent);
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                //status = response.StatusCode,
                data = content
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync(apiUrl + "/user/" + id);
            var content = await response.Content.ReadAsStringAsync();
            //UserAccountModel user = JsonSerializer.Deserialize<UserAccountModel>(content);

            return Ok(new
            {
                //status = response.StatusCode,
                data = content
            });
        }
    }
}