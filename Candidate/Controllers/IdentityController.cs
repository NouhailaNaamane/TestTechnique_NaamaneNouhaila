﻿using Business.Interfaces.Admin;
using Business.Interfaces.Identity;
using Data.Access.Layer.Models;
using Data.Access.Layer.UnitOfWorks;
using Data.Transfer.Object.Administration;
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

        private List<Claim> GenerateClaims(AdminDTO admin)
        {
            // Create the claims for the user
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Id),
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