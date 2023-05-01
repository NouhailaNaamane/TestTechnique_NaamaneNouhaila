using System.ComponentModel.DataAnnotations;

namespace Data.Access.Layer.Models
{
    public class Offre
    {
        public Guid IdOffre { get; set; } = Guid.NewGuid();
        public Guid AddedBy { get; set; } = default!;
        [MaxLength(1000)]
        [MinLength(100)]
        public string? Description { get; set; }
        [StringLength(150)]
        public string Titre { get; set; } = default!;
        public DateTime DatePublish { get; set; } = DateTime.UtcNow;
        public DateTime? DateFin { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MinSalary { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MaxSalary { get; set; }

        [Range(0, 60)]
        public int MinNbrExperience { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Candidature> Candidatures { get; set; }
        public virtual Admin AddedByAdmin { get; set; }

    }
}
