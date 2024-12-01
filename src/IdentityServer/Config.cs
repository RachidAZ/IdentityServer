using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            //new IdentityResource("roles", "Your role(s)", new List<string>() { "StoreManager" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { new ApiScope(name: "Store.Read", displayName: "Read Store") ,
            new ApiScope(name: "Store.Manage", displayName: "Manage Store")
            };

    public static IEnumerable<Client> Clients =>
        new Client[] 
            {
                // machine to machine client
             new Client
                {
                    ClientId = "client1",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("123456789".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "Store.Read"  }
                },
              new Client
                {
                    ClientId = "client3",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("123456789".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "Store.Read" , "Store.Manage" }
                },

             // interactive ASP.NET Core Web App
        new Client
        {
            ClientId = "client2",
            ClientSecrets = { new Secret("123456789".Sha256()) },
            RequirePkce = false,
            
            AllowedGrantTypes = GrantTypes.Code,
            
            // where to redirect to after login
            RedirectUris = { "https://oauth.pstmn.io/v1/callback" , "http://localhost:5001/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "Store.Read" , "Store.Manage"
            }
        }
            };
}