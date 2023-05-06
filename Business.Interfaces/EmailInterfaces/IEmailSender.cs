using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.EmailInterfaces
{
    public interface IEmailSender
    {

        Task<bool> SendMail(string[] To, string Subject, string Body);
        Task<bool> SendMail<T>(string[] To, string Subject, string EmailTemplate, Dictionary<TEMPLATE_KEYS, string> TemplateVariablesValues);
    }

    public enum TEMPLATE_KEYS
    {
        TOKEN,
        USER_LASTNAME,
        USER_FIRSTNAME
    }

}
