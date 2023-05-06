using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Transfer.Object.Identity
{
    public class ResetPasswordDTO
    {
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
        public required string Token { get; set; }
    }
}
