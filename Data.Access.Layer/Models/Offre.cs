using System.ComponentModel.DataAnnotations;

namespace Candidate.Models
{
    public class Offre
    {
        public Guid IdOffres { get; set; } = Guid.NewGuid();
        public string? Description { get; set; }
        public string Titre { get; set; } = default!;
        public DateTime DatePublish { get; set; } = DateTime.UtcNow;
        public DateTime? DateFin { get; set; }    
        public decimal MinSalary { get; set; }  
        public decimal? MaxSalary { get; set; }
        public int MinNbrExperience { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Candidature> Candidatures { get; set; }

    }
}
