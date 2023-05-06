using Business.Interfaces.EmailInterfaces;
using Business.Interfaces.Identity;
using Business.Services.Resources;
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
        private readonly UserManager<Data.Access.Layer.Models.Admin> _userManager;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public IdentityService(SignInManager<Data.Access.Layer.Models.Admin> signManager, UserManager<Data.Access.Layer.Models.Admin> userManager, IUnitOfWork unitOfWork, IEmailSender emailSender) {
            _signInManager = signManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public async Task<bool> CanLogIn(string Login, string Password, bool RememberMe = false)
        {
            var dbAdmin = _unitOfWork.AdminRepository.GetAdminByLogin(Login);

            if (dbAdmin == null)
                return false;

            var identityResult = await _signInManager.PasswordSignInAsync(dbAdmin, Password, RememberMe, true);

            return identityResult.Succeeded;
        }

        public async Task<bool> PasswordForgotten(string Email)
        {
            var dbAdmin = await _unitOfWork.AdminRepository.Get(A => !string.IsNullOrEmpty(A.Email) && A.Email.ToUpper() == Email.ToUpper());

            if (dbAdmin == null)
                throw new Exception("Cet email n'est affecté à aucun compte.");

            if (dbAdmin.Email == null)
                throw new Exception("Cet admin n'a pas d'email assigné à son compte");

            var token = await _userManager.GeneratePasswordResetTokenAsync(dbAdmin);

            Dictionary<TEMPLATE_KEYS, string> emailVariableValues = new Dictionary<TEMPLATE_KEYS, string>()
            {
                { TEMPLATE_KEYS.TOKEN, token },
                { TEMPLATE_KEYS.USER_FIRSTNAME, dbAdmin.Prenom },
                { TEMPLATE_KEYS.USER_LASTNAME, dbAdmin.Nom }
            };

            return await _emailSender.SendMail<TemplateResources>(
                To: new string[] { dbAdmin.Email },
                Subject: "Réinitialisation du mot de passe",
                EmailTemplate: TemplateResources.PasswordForgottenTemplate,
                TemplateVariablesValues: emailVariableValues
                );
        }

    }
}
