namespace BankApplication.Infrastructure.AuthService;

public interface ISmsService
{
    Task<string> SendSmsCodeAsync(string number);
}