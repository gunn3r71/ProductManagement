using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs.Input;
using ProductManagement.Business.Interfaces;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(INotificador notificador,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager) : base(notificador)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                return Ok(registerUser);
            }

            foreach (var error in result.Errors)
                NotifyError(error.Description);

            return BadRequest(registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser login)
        {
            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, false, true);

            if (result.Succeeded)
                return Ok(login);

            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return BadRequest(login);
            }

            NotifyError("Usuário ou senha incorretos");
            return BadRequest(login);
        }
    }
}