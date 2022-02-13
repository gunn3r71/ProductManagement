using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductManagement.API.Controllers;
using ProductManagement.API.DTOs.Input;
using ProductManagement.API.DTOs.Output;
using ProductManagement.API.Extensions;
using ProductManagement.API.Security;
using ProductManagement.Business.Interfaces;

namespace ProductManagement.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _configuration;

        public AuthController(INotificador notificador, 
            IUser appUser,
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> configuration) : base(notificador, appUser)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            var user = new IdentityUser()
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var tokenHandler = new TokenHandler(_configuration, _userManager);

                return Ok(new CustomResponseOutput
                {
                    Success = true,
                    Message = "Registro realizado com sucesso.",
                    Data = await tokenHandler.GenerateTokenAsync(registerUser.Email)
                });
            }

            foreach (var error in result.Errors)
                NotifyError(error.Description);

            return CustomErrorResponse();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser login)
        {
            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, true);

            if (result.Succeeded)
            {
                var tokenHandler = new TokenHandler(_configuration, _userManager);
                return Ok(new CustomResponseOutput
                {
                    Success = true,
                    Message = "Login realizado com sucesso.",
                    Data = await tokenHandler.GenerateTokenAsync(login.Email)
                });
            }

            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomErrorResponse();
            }

            NotifyError("Usuário ou senha incorretos");
            return CustomErrorResponse();
        }
    }
}