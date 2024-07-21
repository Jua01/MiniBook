using Microsoft.AspNetCore.Mvc;
using MiniBook.Server.Shared;

namespace MiniBook.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("get")]
        
        public IActionResult Get()
        {
            return this.OkResult("HomeController");
        }
    }
}
