namespace BankApplication.Infrastructure.AuthService.SmsService;

public interface ISmsService
{
    string SendSmsCodeAsync(string number);
}