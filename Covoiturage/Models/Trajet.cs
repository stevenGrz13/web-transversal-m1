using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covoiturage.Models;

[Table("trajet")]
public class Trajet
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idvehicule")] 
    [DisplayName("Vehicule")]
    public int IdVehicule { get; set; }
    
    [Column("depart")] 
    public string? Depart { get; set; }
    
    [Column("arrivee")] 
    public string? Arrivee { get; set; }
    
    [Column("datedepart")] 
    public DateTime? DateDepart { get; set; }
    
    [Column("prixuniqueplace")]
    public int? PrixUniquePlace { get; set; }
    
    [ForeignKey("IdVehicule")]
    public virtual Vehicule? Vehicule { get; set; }
}