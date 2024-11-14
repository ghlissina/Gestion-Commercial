using CommercialBackend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CommercialBackend.sqlDb
{
    public class DataContext : IdentityDbContext<UserApp>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            // Configuration des relations entre les entités (si nécessaire)

            builder.Entity<DevisArticle>()
           .HasKey(da => new { da.DevisId, da.ArticleId });
        }
    
      
        public DbSet<Article> Articles { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CommandeArticle> CommandeArticles { get; set; }
        public DbSet<BonEntree> BonsEntree { get; set; }
        public DbSet<BonEntreeArticle> BonEntreeArticles { get; set; }
        public DbSet<BonSortie> BonsSortie { get; set; }
        public DbSet<BonSortieArticle> BonSortieArticles { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Devis> Devis { get; set; }
        public DbSet<Client> Clients { get; set; }
        
            public DbSet<DevisArticle> DevisArticles { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<CategorieArticle> CategorieArticles { get; set; }
    }
    }
