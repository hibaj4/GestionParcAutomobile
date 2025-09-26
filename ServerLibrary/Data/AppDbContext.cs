using Microsoft.EntityFrameworkCore;
using SharedLibrary.Entities;

namespace ServerLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Affectation> Affectations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<DocumentAdministratif> DocumentAdministratifs { get; set; }
        public DbSet<Entretien> Entretiens { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Trajet> Trajets { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }



    }
}
