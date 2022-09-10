using BankApplication.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApplication.Models;

public class Profile
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }

    [ForeignKey("AvatarModel")]
    public int? AvatarModelId { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
        
    [StringLength(20)]
    public string Name { get; set; }
    
    [StringLength(20)]
    public string Surname { get; set; }

    public List<Card> Cards { get; set; } = new();
    
    public ProfileStatus Status { get; set; }
    
    public User User { get; set; }

    public AvatarModel AvatarModel { get; set; }
}