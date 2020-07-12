﻿using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using System.Text.Json;
using System;
using business_manager_orchestrator.Clients;
using System.Text;
using business_manager_common_library;

namespace business_manager_orchestrator.Controllers
{
    [Route("business")]
    [ApiController]
    public class BusinessDataController : Controller
    {
        private readonly string apiUrl;
        private readonly AuthClient authClient;

        private readonly IHttpClientFactory _httpClientFactory;
        public BusinessDataController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            authClient = new AuthClient();
            apiUrl = Environment.GetEnvironmentVariable("apiUrl");
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(BusinessModel businessDataModel)
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            string jsonString = JsonSerializer.Serialize(businessDataModel);
            
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await apiClient.PostAsync(apiUrl + "/business", stringContent);
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
        public async Task<IActionResult> GetUser(int id)
        {
            //retrieve access token
            var tokenResponse = await authClient.GetToken();

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync(apiUrl + "/business/" + id);
            var content = await response.Content.ReadAsStringAsync();

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