using Candidate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)   
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidat>()
            .HasKey(c => c.IdCandidat) ;

            modelBuilder.Entity<Candidature>()
            .HasKey(c => new { c.IdCandidat, c.IdOffre });
            
            modelBuilder.Entity<Offre>()
            .HasKey(c => c.IdOffres );

            modelBuilder.Entity<Candidature>()
           .HasOne(c => c.Candidat)
           .WithMany(c => c.Candidatures)
           .HasForeignKey(c => c.IdCandidat);

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Offre)
                .WithMany(o => o.Candidatures)
                .HasForeignKey(c => c.IdOffre);
        }
        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Offre> Offres { get; set; }

    }
}
