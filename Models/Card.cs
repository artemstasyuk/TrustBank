using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Models;

public enum CardStatus
{
    Active,
    Deleted,
    Returned,
    Blocked
}

public class Card
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Profile")]
    public int ProfileId { get; set; }
    
    public string CardNumber { get; set; }
    
    public string CVV { get; set; }
    
    public string CardName { get; set; }
    
    public string CardSurname { get; set; }
    
    [Display(Name = "Balance")]
    public double Balance { get; set; }

    [BindNever]
    public CardStatus CardStatus { get; set; }
       
    //not into db
    public Profile Profile { get; set; }
}