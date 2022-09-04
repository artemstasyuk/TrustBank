namespace BankApplication.Infrastructure.AuthService;

public interface ISmsService
{
    string SendSmsCodeAsync(string number);
}