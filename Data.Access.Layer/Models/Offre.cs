using System.ComponentModel.DataAnnotations;

namespace Data.Access.Layer.Models
{
    public class Offre
    {
        public Guid IdOffre { get; set; } = Guid.NewGuid();
        public Guid AddedBy { get; set; } = default!;
        public string? Description { get; set; }
        public string Titre { get; set; } = default!;
        public DateTime DatePublish { get; set; } = DateTime.UtcNow;
        public DateTime? DateFin { get; set; }    
        public decimal MinSalary { get; set; }  
        public decimal? MaxSalary { get; set; }
        public int MinNbrExperience { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Candidature> Candidatures { get; set; }
        public virtual Admin AddedByAdmin { get; set; }

    }
}
