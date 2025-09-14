using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covoiturage.Models;

[Table("vehicule")]
public class Vehicule
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idutilisateur")]
    [DisplayName("Utilisateur")]
    public int IdUtilisateur { get; set; }
    
    [Column("marque")] 
    public string? Marque { get; set; }
    
    [Column("modele")]
    public string? Modele { get; set; }
    
    [Column("plaque")]
    public string? Plaque { get; set; }
    
    [Column("nombreplace")]
    public int? NombrePlace { get; set; }
    
    [ForeignKey("IdUtilisateur")]
    public virtual Utilisateur? Utilisateur { get; set; }
}