using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Models;

public class Operation
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Card")]
    public int CardId { get; set; }
    
    public double Amount { get; set; }

    public string RecipientСardNumber { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public Card Card { get; set; }
}