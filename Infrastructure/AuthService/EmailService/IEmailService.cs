namespace BankApplication.Infrastructure.AuthService.EmailService;

public interface IEmailService
{
    EmailTokenDto SendEmailCode(string email, string body, string subject);
}