using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Models
{
    public class ApplicationContext : IdentityDbContext<Admin, IdentityRole<Guid>, Guid>
    {
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)   
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<Guid>>().HasKey(p => p.Id);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(p => p.UserId);
            modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(p => p.UserId);
            modelBuilder.Ignore<IdentityUser<Guid>>();

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                .HasName("pk_admin");

                entity.ToTable("Admins");

                entity.HasMany(a => a.Offers)
                .WithOne(a => a.AddedByAdmin)
                .HasForeignKey(a => a.AddedBy)
                .HasConstraintName("fk_admin_offers")
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Offre>(entity =>
            {
                entity.HasKey(e => e.IdOffre)
                .HasName("pk_offre");

                entity.ToTable("Offres");
            });

            modelBuilder.Entity<Candidature>(entity =>
            {
                entity.HasKey(e => new { e.IdOffre, e.NomCandidat, e.PrenomCandidat })
                .HasName("pk_candidateur");

                entity.ToTable("Candidatures");

                entity.HasOne(a => a.Offre)
                .WithMany(p => p.Candidatures)
                .HasForeignKey(d => d.IdOffre)
                .HasConstraintName("fk_candidature_offer")
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
