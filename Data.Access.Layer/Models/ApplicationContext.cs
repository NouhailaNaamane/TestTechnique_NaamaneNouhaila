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
    public class ApplicationContext : IdentityDbContext<Identity>
    {
        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Identity> Identities { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)   
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(p => p.Id);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => p.UserId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => p.UserId);
            modelBuilder.Ignore<IdentityUser<string>>();

            modelBuilder.Entity<Identity>(entity =>
            {
                entity.HasKey(e => e.IdIdentity)
                .HasName("pk_identity");

                entity.ToTable("Identities");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                .HasName("pk_admin");

                entity.ToTable("Admins");

                entity.HasOne(a => a.Identity)
                .WithMany(p => p.Admins)
                .HasForeignKey(d => d.IdIdentity)
                .HasConstraintName("fk_admin_identity_id")
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(a => a.Offers)
                .WithOne(a => a.AddedByAdmin)
                .HasForeignKey(a => a.AddedBy)
                .HasConstraintName("fk_admin_offers")
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Candidat>(entity =>
            {
                entity.HasKey(e => e.IdCandidat)
                .HasName("pk_candidat");

                entity.ToTable("Candidats");

                entity.HasOne(a => a.Identity)
                .WithMany(p => p.Candidats)
                .HasForeignKey(d => d.IdIdentity)
                .HasConstraintName("fk_candidat_identity_id")
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
                entity.HasKey(e => new { e.IdOffre, e.IdCandidat })
                .HasName("pk_candidateur");

                entity.ToTable("Candidatures");

                entity.HasOne(a => a.Offre)
                .WithMany(p => p.Candidatures)
                .HasForeignKey(d => d.IdOffre)
                .HasConstraintName("fk_candidature_offer")
                 .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Candidat)
                .WithMany(p => p.Candidatures)
                .HasForeignKey(d => d.IdOffre)
                .HasConstraintName("fk_candidature_candidat")
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
