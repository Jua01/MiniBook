namespace BlockChainAlert.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlockChainAlert.Entities;
using BlockChainAlert.Helpers;
using BlockChainAlert.Models;
using User = Models.User;
using User2 = Entities.User;
using Microsoft.AspNetCore.Identity;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    Task<User> GetByEmail(string email);


}

public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<User> _users = new List<User>
    {
        //new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
    };

    private readonly AppSettings _appSettings;
    SignInManager<User> signInManager;
    UserManager<User> userManager;

    public UserService(IOptions<AppSettings> appSettings,SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _appSettings = appSettings.Value;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,false,false);

        // Không tìm thấy user
        if (result == null) return null;

        // Tạo token
        var token = generateJwtToken(model);

        return new AuthenticateResponse(model, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public async Task<User> GetByEmail(string email)
    {
        var result = await userManager.FindByEmailAsync(email);
        
        return result;
    }

    // helper methods

    private string generateJwtToken(AuthenticateRequest user)
    {
        // Token tồn tại trong 1 ngày
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("Email", user.Email.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}