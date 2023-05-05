using Business.Interfaces.TemplateInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Business.Interfaces.EmailInterfaces;

namespace Business.Services.TemplateService
{
    public class TemplateService : ITemplateService
    {
        private IConfiguration _configuration { get; }

        public TemplateService(IConfiguration configuration) { 
            _configuration = configuration;
        }

        public string ReadEmailTemplateAndReplaceValues(string emailTemplate, Dictionary<TEMPLATE_KEYS, string> templateVariableValues)
        {
            this.ReplaceCommunEmailTemplateVariable(ref emailTemplate!);
            this.ReplaceEmailTemplateVariable(ref emailTemplate!, templateVariableValues);

            return emailTemplate;
        }

        private void ReplaceCommunEmailTemplateVariable(ref string emailTemplate)
        {
            foreach (var variable in Enum.GetNames(typeof(TEMPLATE_COMMUN_KEYS)))
                emailTemplate = emailTemplate.Replace($"{{{{ {variable} }}}}", this.TemplateVariableValue((TEMPLATE_COMMUN_KEYS)Enum.Parse(typeof(TEMPLATE_COMMUN_KEYS), variable)));
        }

        private void ReplaceEmailTemplateVariable(ref string emailTemplate, Dictionary<TEMPLATE_KEYS, string> templateVariableValues)
        {
            foreach (var item in templateVariableValues)
                emailTemplate = emailTemplate.Replace($"{{{{ {item.Key} }}}}", item.Value);
        }

        private string? TemplateVariableValue(TEMPLATE_COMMUN_KEYS key) =>
            key switch
            {
                TEMPLATE_COMMUN_KEYS.DOMAIN_NAME => _configuration.GetValue<string>("EnvironmentSettings:DomainName"),
                TEMPLATE_COMMUN_KEYS.APP_NAME => _configuration.GetValue<string>("EnvironmentSettings:ApplicationName"),
                _ => string.Empty
            };

    }

    public enum TEMPLATE_COMMUN_KEYS
    {
        DOMAIN_NAME,
        APP_NAME
    }
}
