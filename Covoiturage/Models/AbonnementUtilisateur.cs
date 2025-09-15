using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covoiturage.Models;

[Table("abonnementutilisateur")]
public class AbonnementUtilisateur
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idutilisateur")]
    [DisplayName("utilisateur")]
    public int IdUtilisateur { get; set; }
    
    [Column("idmois")]
    public int IdMois { get; set; }
    
    [Column("annee")]
    public int Annee { get; set; }
    
    [Column("datepaiement")]
    public DateTime DatePaiement { get; set; }
    
    [ForeignKey("IdUtilisateur")]
    public virtual Utilisateur? Utilisateur { get; set; }
}