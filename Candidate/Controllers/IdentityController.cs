using Business.Interfaces.Admin;
using Business.Interfaces.Identity;
using CvThèque.Extensions;
using Data.Access.Layer.Models;
using Data.Access.Layer.UnitOfWorks;
using Data.Transfer.Object.Administration;
using Data.Transfer.Object.Identity;
using Data.Transfer.Object.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CvThèque.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;


        public IdentityController(IIdentityService identityService, IAdminService adminService, IConfiguration configuration)
        {
            _identityService = identityService;
            _adminService = adminService;
            _configuration = configuration;
        }


        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([FromBody] LoginPasswordDTO loginCredentials)
        {
            if(User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");

            if (ModelState.IsValid)
            {
                var canSignIn = await this._identityService.CanLogIn(loginCredentials.Login, loginCredentials.Password, loginCredentials.RememberMe);

                if(canSignIn)
                {
                    var connectedUser = _adminService.GetAdminByLogin(loginCredentials.Login)!;

                    var token = this.GenerateToken(GenerateClaims(connectedUser)); //Generate token from the specified claims

                    this.StoreTokenInCookie(token);

                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError(string.Empty, "Votre identifiant ou mot de passe est incorrect.");
            }
            return View(loginCredentials);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SendRenewPassword([FromBody] PasswordForgottenDTO passwordForgotten)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");

            if (ModelState.IsValid)
            {
                var isTokenGeneratedAndSent = await this._identityService.PasswordForgotten(passwordForgotten.Email);

                if (!isTokenGeneratedAndSent)
                    ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la génération du token ou envoi à votre adresse email, veuillez vérifier plus tard");

                return View();
            }
            else
                return View(passwordForgotten);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPassword)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");

            if (ModelState.IsValid)
            {
                var isPasswordReseted = await this._identityService.RenewForgottenPassword(resetPassword.Token, resetPassword.Email, resetPassword.NewPassword);

                if (!isPasswordReseted)
                    ModelState.AddModelError(string.Empty, "Nous n'avons pas pu renouveller votre mot de passe, merci de vérifier vos données.");

                return View();
            }
            else
                return View(resetPassword);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            if (ModelState.IsValid)
            {
                var isPasswordReseted = await this._identityService.UpdatePassword(this.GetIdAdmin(), changePassword.OldPassword, changePassword.NewPassword);

                if (!isPasswordReseted)
                    ModelState.AddModelError(string.Empty, "Nous n'avons pas pu changer votre mot de passe, merci de vérifier vos données.");

                return View();
            }
            else
                return View(changePassword);
        }

        private List<Claim> GenerateClaims(AdminDTO admin)
        {
            // Create the claims for the user
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Id),
                new Claim(JwtRegisteredClaimNames.Iss, admin.IdAdmin.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, admin.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }

        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            // Create the JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );
        }

        private void StoreTokenInCookie(JwtSecurityToken token)
        {
            // Store the JWT token in a cookie
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Response.Cookies.Append("jwt", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });
        }

    }
}
