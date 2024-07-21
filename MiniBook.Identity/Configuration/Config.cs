using IdentityServer4.Models;
namespace MiniBook.Identity.Configuration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "Main API Resource")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api","openid", "profile", "email" },
                    AllowOfflineAccess = true,
                },
            };
        }
        public static List<ApiScope> GetApiScope()
        {
            return new List<ApiScope>
       {
           new ApiScope
           {
               Name = "api",
               //Enabled = true,
               Emphasize=false,
           },

       };
        }

    }
}
