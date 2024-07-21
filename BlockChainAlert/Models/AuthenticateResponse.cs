using BlockChainAlert.Entities;

namespace BlockChainAlert.Models
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(AuthenticateRequest user, string token)
        {
            FirstName = user.Email;
            LastName = user.Email;
            Email = user.Email;
            Token = token;
        }
    }
}
