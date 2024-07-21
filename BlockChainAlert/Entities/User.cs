
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace BlockChainAlert.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        //[JsonIgnore]
        //public string Password { get; set; }
    }
}
