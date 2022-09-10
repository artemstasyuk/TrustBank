namespace BankApplication.Dto;

public class EmailTokenDto
{
    public string EmailCode { get; set; }
    public string Email { get; set; }

    public virtual void SetEmailToken(EmailTokenDto emailTokenDto)
    {
        Email = emailTokenDto.Email;
        EmailCode = emailTokenDto.EmailCode;        
    }
}