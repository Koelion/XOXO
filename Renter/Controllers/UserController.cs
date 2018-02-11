using Authorization;
using Database.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Login;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Renter.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepositoryService userRepositoryService;
        private readonly ITokenProvider tokenProvider;

        public UserController(IUnitOfWork unitOfWork,
            IUserRepositoryService userRepositoryService,
            ITokenProvider tokenProvider)
        {
            this.unitOfWork = unitOfWork;
            this.userRepositoryService = userRepositoryService;
            this.tokenProvider = tokenProvider;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] LoginDto loginDto)
        {
            var token = tokenProvider.GenerateToken(loginDto.Username, loginDto.Password);
            var user = userRepositoryService.FindUserByUsername(loginDto.Username);
            if (token == null)
                return NotFound("Bad username or password");
            token.User = user;

            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, user.Role.Name),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(new[] { claimsIdentity });
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Json(token);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return View();
        }
    }
}