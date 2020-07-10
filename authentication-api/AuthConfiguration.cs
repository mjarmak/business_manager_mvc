using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace business_manager_api
{
    public static class AuthConfiguration
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource("business_manager_api")
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
                    AllowedScopes = { "business_manager_api" }
                }
            };
        }

        //public static IEnumerable<ApiScope> GetScopes()
        //{
        //    return new List<ApiScope> {
        //        new ApiScope("business_manager", "Business Manager")
        //    };
        //}
    }
}
