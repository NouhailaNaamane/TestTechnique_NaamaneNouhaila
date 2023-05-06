using Data.Transfer.Object.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<bool> CanLogIn(string Email, string Password, bool RememberMe = false);
        Task<bool> PasswordForgotten(string Email);
        Task<bool> RenewForgottenPassword(string Token, string Email, string NewPassword);
        Task<bool> UpdatePassword(string IdentityID, string OldPassword, string NewPassword); 
    }
}
