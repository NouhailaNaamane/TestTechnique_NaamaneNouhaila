using Business.Interfaces.EmailInterfaces;
using Business.Interfaces.TemplateInterface;
using Business.Services.Resources;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration _configuration { get; }
        private ITemplateService _templateService { get; }

        public EmailSender(IConfiguration configuration, ITemplateService templateService)
        {
            _configuration = configuration;
            _templateService = templateService;
        }

        public async Task<bool> SendMail(string[] To, string Subject, string Body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_configuration.GetValue<string>("Mailing:Host"), _configuration.GetValue<int>("Mailing:Port")))
                {
                    smtpClient.Credentials = new NetworkCredential(_configuration.GetValue<string>("Mailing:SenderMail"), _configuration.GetValue<string>("Mailing:SenderPassword"));
                    smtpClient.UseDefaultCredentials = true;

                    var mailMessage = new MailMessage()
                    {
                        From = new MailAddress(_configuration.GetValue<string>("Mailing:SenderMail")!),
                        Subject = Subject,
                        Body = Body,
                        IsBodyHtml = true
                    };

                    foreach (var item in To)
                        mailMessage.To.Add(item);

                    await smtpClient.SendMailAsync(mailMessage);

                    return true;
                }

            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendMail<T>(string[] To, string Subject, string emailTemplate, Dictionary<TEMPLATE_KEYS, string> TemplateVariablesValues)
        {
            string body = _templateService.ReadEmailTemplateAndReplaceValues(emailTemplate, TemplateVariablesValues);

            return await this.SendMail(To, Subject, body);
        }
    }
}
