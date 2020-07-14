using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using business_manager_orchestrator.Clients;
using System;

namespace business_manager_orchestrator.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthClient authClient;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            authClient = new AuthClient();
        }

        [Route("/test")]
        public async Task<String> Index()
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            //retrieve secret data
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:44345/weatherforecast/all");
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}