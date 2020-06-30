using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace business_manager_api
{
    public static class DefaultConfiguration
    {
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> {
                new ApiResource("business_manager_api")
            };
        public static IEnumerable<Client> GetClients() =>
            new List<Client> {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "business_manager" }
                }
            };
    }
}
