using Covoiturage.Models;
using Microsoft.EntityFrameworkCore;

namespace Covoiturage;

public class CovoiturageContext : DbContext
{
    public CovoiturageContext(DbContextOptions<CovoiturageContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<Trajet> Trajets { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Vehicule> Vehicules { get; set; }
    public DbSet<Voyage> Voyages { get; set; }
    public DbSet<Depense> Depenses { get; set; }
    public DbSet<Commission> Commission { get; set; }
}