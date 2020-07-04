using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace business_manager_api
{
    public static class AuthConfiguration
    {
        //public static IEnumerable<IdentityResource> GetIdentityResources() =>
        //    new List<IdentityResource>
        //    {
        //        new IdentityResources.OpenId()
        //    };

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
                },
                //new Client {
                //    ClientId = "client_id_mvc",
                //    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,

                //    RedirectUris = { "https://localhost:44322/signin-oidc" }, //angular login page
                //    PostLogoutRedirectUris = { "https://localhost:44322/Home/Index" }, //angular home page

                //    AllowedScopes = {
                //        "ApiOne",
                //        "ApiTwo",
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        "rc.scope",
                //    },
                //    AllowOfflineAccess = true,
                //    RequireConsent = false,
                //},
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
