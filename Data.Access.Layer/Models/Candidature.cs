using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Layer.Models
{
    public class Candidature
    {
        public Guid IdOffre { get; set; } = default!;
        public DateTime DateCandidature { get; set; } = DateTime.UtcNow;
        [StringLength(30)]
        public string NomCandidat { get; set; } = default!;
        [StringLength(30)]
        public string PrenomCandidat { get; set; } = default!;
        [EmailAddress]
        public string EmailCandidat { get; set; } = default!;
        [Phone]
        public string PhoneCandidat { get; set; } = default!;
        public string NiveauEtude { get; set; } = default!;
        public int NbAnneeExpr { get; set; } = 0;
        public string DernierEmployeur { get; set; } = default!;
        public string? CvFileName { get; set; }

        public virtual Offre Offre { get; set; }

    }
}
