using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Covoiturage.Models;

[Table("voyage")]
public class Voyage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idpassager")] 
    [DisplayName("utilisateur")]
    public int IdPassager { get; set; }
    
    [Column("idtrajet")]
    [DisplayName("trajet")]
    public int IdTrajet { get; set; }
    
    [Column("lieurecuperation")] 
    public string? LieuRecuperation { get; set; }
    
    [Column("destination")] 
    public string? Destination { get; set; }
    
    [Column("estpayee")] public bool? EstPayee { get; set; } = false;
    
    [ForeignKey("IdPassager")]
    public virtual Utilisateur? Utilisateur { get; set; }
    
    [ForeignKey("IdTrajet")]
    public virtual Trajet? Trajet { get; set; }
}