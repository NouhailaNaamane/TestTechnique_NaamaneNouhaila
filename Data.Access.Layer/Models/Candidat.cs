using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Access.Layer.Models
{
    public class Candidat
    {
        public Guid IdCandidat { get; set; } = Guid.NewGuid();
        public Guid IdIdentity { get; set; } = default!;
        public string NiveauEtude { get; set; } = default!;
        public int NbAnneeExpr { get; set; }
        public string DernierEmployeur { get; set; } = default!;
        public string? CvFileName { get; set; }

        public virtual ICollection<Candidature> Candidatures { get; set; }
        public virtual Identity Identity { get; set; } = default!;

    }
}
