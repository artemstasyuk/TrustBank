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
    
    public EmailTokenDto SendEmailCode(string emailTo)
    {

        MailMessage message = new MailMessage();

            message.IsBodyHtml = false; 
            
            message.From = new MailAddress(_configuration.GetSection("GmailData:MailAddress").Value,
                _configuration.GetSection("GmailData:Name").Value);
            
            message.To.Add(new MailAddress(emailTo)); // to
            
            message.Subject = "EmailVerification"; // message title
            
            message.Body =  GenerateEmailCode(); // message body

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)) // using Google server
            {
                client.Credentials = new NetworkCredential(
                    _configuration.GetSection("GmailData:MailAddress").Value, // email
                    _configuration.GetSection("GmailData:Password").Value); // passwor
                client.EnableSsl = true; //SSL обязательно
                client.Send(message);
            }

            return new EmailTokenDto() {Email = emailTo, EmailCode = message.Body};
    }
    private string GenerateEmailCode()
    {
        int _min = 000000;
        int _max = 999999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max).ToString();
    }
}