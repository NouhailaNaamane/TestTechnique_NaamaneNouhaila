using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Models
{
    public class Admin
    {
        public Guid IdAdmin { get; set; }
        public bool IsSuperAdmin { get; set; } = false;
        public Guid IdIdentity { get; set; } = default!;

        public virtual Identity Identity { get; set; }
        public virtual ICollection<Offre> Offers { get; set; }
    }
}
