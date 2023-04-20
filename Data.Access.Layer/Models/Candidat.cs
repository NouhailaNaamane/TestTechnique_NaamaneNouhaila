using System.ComponentModel.DataAnnotations;

namespace Candidate.Models
{
    public class Candidat
    {
        public Guid IdCandidat { get; set; } = Guid.NewGuid();
        public string Nom { get; set; } = default!;
        public string Prenom { get; set; } = default!;
        public string Mail { get; set; } = default!;
        public string? Telephone { get; set; }
        public string NiveauEtude { get; set; } = default!;
        public int NbAnneeExpr { get; set; }
        public string DernierEmployeur { get; set; } = default!;
        public string? CvFileName { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Candidature> Candidatures { get; set; }

    }
}
