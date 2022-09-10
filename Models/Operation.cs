using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Models;

public enum CardOperationType{
    Transfer,
    Replenish
}

public class Operation
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("CardFrom")]
    public int CardFromId { get; set; }
    
    [ForeignKey("CardTo")]
    public int CardToId { get; set; }
    
    public decimal Amount { get; set; }

    public string RecipientСardNumber { get; set; }
    
    public bool IsCompleted { get; set; }

    public CardOperationType CardOperationType { get; set; }

    public Card CardTo { get; set; }
    
    public Card CardFrom { get; set; }
}