using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace business_manager_orchestrator.Clients
{
    public class AuthClient
    {
        private string authUrl;
        private HttpClient serverClient;
        public AuthClient()
        {
            serverClient = new HttpClient();

            authUrl = Environment.GetEnvironmentVariable("authUrl");
        }
        public async Task<TokenResponse> GetToken()
        {
            //retrieve access token
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync(authUrl);
            return await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "business_manager_api",
                    GrantType = "client_credentials"
                });
        }
    }
}
