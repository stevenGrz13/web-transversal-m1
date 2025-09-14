using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covoiturage.Models;

[Table("utilisateur")]
public class Utilisateur
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idrole")]
    [DisplayName("role")]
    public int IdRole { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("email")]
    public string? Email { get; set; }
    
    [Column("motdepasse")]
    public string? MotDePasse { get; set; }
    
    [Column("numero")]
    public string? Numero { get; set; }
    
    [Column("createdat")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("IdRole")]
    public virtual Role? Role { get; set; }
}