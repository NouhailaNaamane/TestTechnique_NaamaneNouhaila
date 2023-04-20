using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Layer.Models
{
    public class Candidature
    {
        public Guid IdOffre { get; set; } = default!;
        public Guid IdCandidat { get; set; } = default!;
        public DateTime DateCandidature { get; set; } = DateTime.UtcNow;

        public virtual Candidat Candidat { get; set; }

        public virtual Offre Offre { get; set; }

    }
}
