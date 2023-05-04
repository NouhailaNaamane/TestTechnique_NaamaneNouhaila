using Business.Interfaces.Identity;
using Data.Access.Layer.Models;
using Data.Access.Layer.UnitOfWorks;
using Data.Transfer.Object.Identity;
using LinqKit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<Data.Access.Layer.Models.Admin> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(SignInManager<Data.Access.Layer.Models.Admin> signManager, IUnitOfWork unitOfWork) {
            _signInManager = signManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CanLogIn(string Login, string Password, bool RememberMe = false)
        {
            var dbAdmin = _unitOfWork.AdminRepository.GetAdminByLogin(Login);

            if (dbAdmin == null)
                return false;

            var identityResult = await _signInManager.PasswordSignInAsync(dbAdmin, Password, RememberMe, true);

            return identityResult.Succeeded;
        }
    }
}
