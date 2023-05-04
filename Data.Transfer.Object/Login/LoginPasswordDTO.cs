using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Transfer.Object.Login
{
    public class LoginPasswordDTO
    {
        [Required]
        public string Login { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; } = false;
    }
}
