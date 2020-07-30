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
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource("bm"),
                new ApiResource("auth", new[] {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role
                })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "bm", "auth", IdentityServerConstants.StandardScopes.OpenId },
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
            };
        }
    }
}
