using System.ComponentModel.DataAnnotations;

namespace BankApplication.Dto;

public class SmsTokenDto
{
    [StringLength(6)]
    public string SmsToken { get; set; }
}