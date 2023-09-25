using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SeatManagement.Models;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace SeatManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if(user.userName.Equals("string") && user.password.Equals("string"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "User")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "MyCookie");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookie", principal);
                return Ok("Login successful");
            }
            else
            {
                await HttpContext.SignOutAsync("MyCookie");
                return Unauthorized("Login first");
            }
        }
    }
}
