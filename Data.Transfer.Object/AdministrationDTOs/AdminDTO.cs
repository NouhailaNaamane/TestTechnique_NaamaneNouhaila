using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Transfer.Object.AdministrationDTOs
{
    public class AdminDTO
    {
        public Guid IdAdmin { get; set; }
        public bool IsSuperAdmin { get; set; } = false;

        [StringLength(30)]
        public string Nom { get; set; } = default!;

        [StringLength(30)]
        public string Prenom { get; set; } = default!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string? PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

    }
}
