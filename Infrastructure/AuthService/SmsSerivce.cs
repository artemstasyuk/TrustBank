using Twilio;
using Twilio.Rest.Chat.V1.Service.Channel;
using Twilio.Types;

namespace BankApplication.Infrastructure.AuthService;

public class SmsService : ISmsService
{
    public async Task<string> SendSmsCodeAsync(string number)
    {
        var code = GenerateSmsCode();
        
        string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

        TwilioClient.Init(accountSid, authToken);

        var message = MessageResource.CreateAsync(
             "This is the ship that made the Kessel Run in fourteen parsecs?",
             "+15017122661",
                     "+15558675310"
        );
        
        return code;
    }

    private string GenerateSmsCode()
    {
        int _min = 000000;
        int _max = 999999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max).ToString();
    }

}