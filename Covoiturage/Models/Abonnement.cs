using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covoiturage.Models;

[Table("abonnement")]
public class Abonnement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("montantpassager")] 
    public double MontantPassager { get; set; }
    
    [Column("montantchauffeur")] 
    public double MontantChauffeur { get; set; }
    
    [Column("datechangement")]
    public DateTime DateChangement { get; set; }
}