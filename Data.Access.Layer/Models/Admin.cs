using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Models
{
    public class Admin : IdentityUser<Guid>
    {
        public Guid IdAdmin { get; set; }
        public bool IsSuperAdmin { get; set; } = false;

        [StringLength(30)]
        public string Nom { get; set; } = default!;

        [StringLength(30)]
        public string Prenom { get; set; } = default!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<Offre> Offers { get; set; }
    }
}
