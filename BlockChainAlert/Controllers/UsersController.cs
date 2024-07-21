using BlockChainAlert.Helpers;
using BlockChainAlert.Models;
using BlockChainAlert.Services;
using BlockChainAlert.ViewModels;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniBook.Server.Shared;

namespace BlockChainAlert.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public UsersController(IUserService userService, UserManager<User> UserManager, SignInManager<User> signInManager)
        {
            _userService = userService;
            userManager = UserManager;
            this.signInManager = signInManager;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return this.ErrorResult(ErrorCode.REGISTER_REQUIRED_EMAIL);
            }
            var user = new User() { Email = model.Email, UserName = model.Email };

            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.GivenName,
                ClaimValue = model.FirstName,
            });

            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.FamilyName,
                ClaimValue = model.LastName,
            });
           

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.OkResult();
            }
            else
            {
                if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
                {
                    return this.ErrorResult(ErrorCode.REGISTER_DUPLICATE_USER_NAME);
                }
                return this.ErrorResult(ErrorCode.BAD_REQUEST);
            };
        }
    }
}
