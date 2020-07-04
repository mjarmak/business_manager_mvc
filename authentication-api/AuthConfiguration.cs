using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace business_manager_api
{
    public static class AuthConfiguration
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource("business_manager_api"),
                new ApiResource("business_manager_orchestrator")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "business_manager" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetScopes()
        {
            return new List<ApiScope> {
                new ApiScope("business_manager", "Business Manager")
            };
        }
    }
}
