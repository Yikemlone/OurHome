using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurHome.Models.Models;
using OurHome.Shared;
using System.Security.Claims;

namespace OurHome.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthorizeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            var user = await _userManager.FindByNameAsync(parameters.UserName);
            if (user == null) return BadRequest("User does not exist");

            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);

            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            try
            {
                await _signInManager.SignInAsync(user, parameters.RememberMe);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
             
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterParameters parameters)
        {
            var user = new User();

            user.Id = Guid.NewGuid();
            user.UserName = parameters.UserName;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.EmailConfirmed = false;
            user.NormalizedUserName = parameters.UserName.ToUpper();

            var result = await _userManager.CreateAsync(user, parameters.Password);

            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            var userClaim = new Claim(ClaimTypes.Role, "User");
            var custSucc = await _userManager.AddClaimAsync(user, userClaim);

            if (!custSucc.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            LoginParameters loginParameters = new LoginParameters
            {
                UserName = parameters.UserName,
                Password = parameters.Password
            };  

            return await Login(loginParameters);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public UserInfo UserInfo()
        {
            return BuildUserInfo();
        }

        private UserInfo BuildUserInfo()
        {
            return new UserInfo
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ExposedClaims = User.Claims
                    .ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
