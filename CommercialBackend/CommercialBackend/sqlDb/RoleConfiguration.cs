using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommercialBackend.sqlDb
{
    
        public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
        {
            public void Configure(EntityTypeBuilder<IdentityRole> builder)
            {
                builder.HasData(
                new IdentityRole
                {
                    Name = "Client",
                    NormalizedName = "CLIENT"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Magasinier",
                    NormalizedName = "MAGASINIER"
                },
                new IdentityRole
                {
                    Name = "Livreur",
                    NormalizedName = "LIVREUR"
                },
                new IdentityRole
                {
                    Name = "Commercial",
                    NormalizedName = "COMMERCIAL"
                });
            }
        }
    
}
