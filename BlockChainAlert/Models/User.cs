using Microsoft.AspNetCore.Identity;

namespace BlockChainAlert.Models
{
    public class User: IdentityUser
    {
        public ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();
    }
}
