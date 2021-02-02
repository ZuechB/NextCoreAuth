using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                //new IdentityResource {
                //    Name = "role",
                //    UserClaims = new List<string> {"role"}
                //}
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("doughnutapi", "Doughnut API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // React client
                new Client
                {
                    ClientId = "wewantdoughnuts",
                    ClientName = "We Want Doughnuts",
                    ClientUri = "http://localhost:3000",

                    AllowedGrantTypes = GrantTypes.Implicit,

                    //AlwaysIncludeUserClaimsInIdToken = true,

                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:3000/signin-oidc",
                        "http://localhost:3000",
                    },

                    PostLogoutRedirectUris = { "http://localhost:3000/signout-oidc" },
                    AllowedCorsOrigins = { "http://localhost:3000" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "doughnutapi"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
        }

        public static List<ApiScope> GetScopes()
        {
            return new List<ApiScope>
           {
               new ApiScope
               {
                   Name = "doughnutapi"
               },
           };
        }
    }


}
