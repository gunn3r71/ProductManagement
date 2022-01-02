﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductManagement.API.DTOs.Input;
using ProductManagement.API.Extensions;
using ProductManagement.API.Security;
using ProductManagement.Business.Interfaces;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _configuration;

        public AuthController(INotificador notificador,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager, 
                              IOptions<JwtSettings> configuration) : base(notificador)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var tokenHandler = new TokenHandler(_configuration);

                return Ok(tokenHandler.GenerateToken());
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
            {
                var tokenHandler = new TokenHandler(_configuration);
                return Ok(tokenHandler.GenerateToken());
            }

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