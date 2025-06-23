using IdentityService.BLL.Options;
using System.Net.Mail;
using System.Net;

namespace IdentityService.API.Extentions
{
    public static class ConfigureOptionsExtension
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(nameof(SmtpOptions)));

            var smtpSettings = builder.Configuration
                .GetSection(nameof(SmtpOptions))
                .Get<SmtpOptions>();

            builder.Services
                .AddFluentEmail(smtpSettings.From)
                .AddSmtpSender(new SmtpClient(smtpSettings.Host)
                {
                    Port = smtpSettings.Port,
                    Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
                    EnableSsl = true
                });
        }
    }
}
