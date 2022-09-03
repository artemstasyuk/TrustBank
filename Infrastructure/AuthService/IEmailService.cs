namespace BankApplication.Infrastructure.AuthService;

public interface IEmailService
{
    EmailTokenDto SendEmailCode(string email);
}