using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Models
{
    public class Identity : IdentityUser
    {
        public Guid IdIdentity { get; set; } = Guid.NewGuid();
        public string Nom { get; set; } = default!;
        public string Prenom { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Candidat> Candidats { get; set; }
    }
}
