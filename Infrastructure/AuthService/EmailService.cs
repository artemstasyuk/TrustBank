using System.Net;
using System.Net.Mail;

namespace BankApplication.Infrastructure.AuthService;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public EmailTokenDto SendEmailCode(string emailTo, string body, string subject)
    {

        MailMessage message = new MailMessage();

            message.IsBodyHtml = false; 
            
            message.From = new MailAddress(_configuration.GetSection("GmailData:MailAddress").Value,
                _configuration.GetSection("GmailData:Name").Value);
            
            message.To.Add(new MailAddress(emailTo)); // to
            
            message.Subject = subject; // message title
            
            message.Body =  body; // message body

            using (SmtpClient client = new SmtpClient("smtp.yandex.ru", 587)) // using Google server
            {
                client.Credentials = new NetworkCredential(
                    _configuration.GetSection("GmailData:MailAddress").Value, // email
                    _configuration.GetSection("GmailData:Password").Value); // passwor
                client.EnableSsl = true; //SSL обязательно
                client.Send(message);
            }

            return new EmailTokenDto() {Email = emailTo, EmailCode = message.Body};
    }
}