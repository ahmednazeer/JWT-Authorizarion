using Jwt.API.Config;
using Jwt.API.Helpers;
using Jwt.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

namespace Jwt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptions<JwtConfig> _jwtConfig;

        public AuthenticationController(UserManager<IdentityUser> userManager,IOptions<JwtConfig>  jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser([FromBody]UserRegisterModel registerModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var existedUser=await _userManager.FindByEmailAsync(registerModel.Email);

            if(existedUser is not null) return BadRequest(ErrorMessages.UserAlreadyExists);
            var newUser = new IdentityUser { Email = registerModel.Email, UserName = registerModel.Username };
            var newCreatedUser= await _userManager.CreateAsync(newUser,registerModel.Password);

            if (!newCreatedUser.Succeeded)
                return BadRequest(newCreatedUser.Errors);
            var key = _jwtConfig.Value.Secret;
            return Ok(new AuthResponseModel { Success = true, Toekn = JwtHelper.GenerateJwtToekn(newUser, key) });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            if(!ModelState.IsValid) return BadRequest(ErrorMessages.InvalidPayload);
            var user=_userManager.FindByEmailAsync(model.Email);
            if (user is null) return BadRequest(ErrorMessages.InvalidPayload);
            var validCredentials =await _userManager.CheckPasswordAsync(user.Result, model.Password);

            if(!validCredentials)
                return BadRequest(ErrorMessages.InvalidSigninCredentials);
            return Ok(
                new AuthResponseModel { Errors=null,Toekn=JwtHelper.GenerateJwtToekn(user.Result, _jwtConfig.Value.Secret)}
                );
        }

    }
}
