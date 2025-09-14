using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covoiturage.Models;

[Table("depense")]
public class Depense
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("intitule")]
    public int Intitule { get; set; }
    
    [Column("montant")]
    public double Montant { get; set; }

    [Column("datepaiement")] 
    public DateTime Date { get; set; }
}