using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RoleBasedAuthorizationAPI.DTOs;
using RoleBasedAuthorizationAPI.Models;
using RoleBasedAuthorizationAPI.Services;

namespace RoleBasedAuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenServices _tokenServices;
        public AuthController(UserManager<ApplicationUser> userManager, TokenServices tokenServices)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTOs model)
        {
            var user = new ApplicationUser
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,

            };
            var result = await _userManager.CreateAsync(user, model.password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok(new
            {
                message = "User Registerd Successfully",
                role = model.Role

            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTOs model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized("Invalid Email or Password ");
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized("Invalid Email or Password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role == null)
            {
                return Unauthorized("User has no role assigned");
            }

            var token = _tokenServices.CreateToken(user, role);

            return Ok(new
            {
                message = "Login Successfull",
                token = token,
                role = role
            });
        }
    }
}
