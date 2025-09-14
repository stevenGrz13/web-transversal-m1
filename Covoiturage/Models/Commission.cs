using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covoiturage.Models;

[Table("commission")]
public class Commission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("pourcentage")]
    public double Pourcentage { get; set; }
    
    [Column("datedecision")]
    public DateTime DateDecision { get; set; }
}