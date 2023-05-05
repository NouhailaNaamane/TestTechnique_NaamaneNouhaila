using Business.Interfaces.EmailInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.TemplateInterface
{
    public interface ITemplateService
    {
        string ReadEmailTemplateAndReplaceValues(string emailTemplate, Dictionary<TEMPLATE_KEYS, string> templateVariableValues);
    }
}
