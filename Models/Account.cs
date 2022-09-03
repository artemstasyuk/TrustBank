using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Twilio.TwiML.Voice;

namespace BankApplication.Models;

public enum AccountStatus
{
    Active,
    Deleted,
    Returned
}

public class Account
{
    [Key]
    public int Id { get; set; }
    
    
    [DataType(DataType.PhoneNumber)]
    [StringLength(13)]   
    public string PhoneNumber { get; set; }
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [StringLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    
    [StringLength(20)]
    public string AccountName { get; set; }

    
    [StringLength(20)]
    public string AccountSurname { get; set; }

    public List<Card> Cards { get; set; } = new();
    
    public AccountStatus AccountStatus { get; set; }

    public bool IsVerified { get; set; } = false;
}