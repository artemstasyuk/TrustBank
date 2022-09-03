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
    
    
    [DataType(DataType.PhoneNumber)]
    [StringLength(13)]      
    public string PhoneNumber { get; set; }
    
    [ForeignKey("Account")]
    public int AccountId { get; set; }
    
    [Display(Name = "CardNumber")]
    [StringLength(16)]
    public string CardNumber { get; set; }
    
    [Display(Name = "CVV")]
    [StringLength(3)]
    public string CVV { get; set; }
    
    [Display(Name = "Name")]
    [StringLength(20)]
    [Required(ErrorMessage = "Name must match the format")]
    public string CardName { get; set; }

                
    [Display(Name = "Surname")]
    [StringLength(20)]
    [Required(ErrorMessage = "Surname must match the format")] 
    public string CardSurname { get; set; }
    
    [Display(Name = "Balance")]
    public double Balance { get; set; }

    [BindNever]
    public CardStatus CardStatus { get; set; }
       
    //not into db
    public Account Account { get; set; }
}