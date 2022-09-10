using BankApplication.Extensions;
using Newtonsoft.Json;

namespace BankApplication.Dto;

public class SessionEmailToken : EmailTokenDto
{
    [JsonIgnore]
    public ISession Session { get; set; }
    
    public static EmailTokenDto GetEmailToken(IServiceProvider serviceProvider)
    {
        ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        SessionEmailToken emailToken = session?.GetJson<SessionEmailToken>("emailToken") ?? new SessionEmailToken();
        emailToken.Session = session;
        return emailToken;
    }

    public override void SetEmailToken(EmailTokenDto emailTokenDto)
    {
        base.SetEmailToken(emailTokenDto);
        Session.SetJson("emailToken", this);
    }
}