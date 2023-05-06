using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CvThèque.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetIdAdmin(this ControllerBase controllerBase)
        {
            return controllerBase.User.Claims.First(C => C.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
