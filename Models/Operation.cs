using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Models;

public enum CardOperationType{
    None,
    Transfer,
    Replenish
}

public class Operation
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Card")]
    public int CardFromId { get; set; }
    
    public int CardToId { get; set; }
    
    public decimal Amount { get; set; }

    public string RecipientСardNumber { get; set; }
    
    public bool IsCompleted { get; set; }

    public CardOperationType CardOperationType { get; set; }

    public Card Card { get; set; }
}