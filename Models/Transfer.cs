using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Models;

public class Transfer
{
    [Key]
    public int Id { get; set; }
    
    
    [ForeignKey("Card")]
    public int CardId { get; set; }
    
    
    [Display(Name = "Payment Amount")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter value more than null")]
    public double Amount { get; set; }
    
    
    public bool IsCompleted { get; set; }
    
    //not into db
    public Card Card { get; set; }
}