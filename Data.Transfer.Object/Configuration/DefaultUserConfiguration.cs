using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Transfer.Object.Configuration
{
    public class DefaultUserConfiguration
    {
        public required string SuperAdminEmail { get; set; }
        public required string SuperAdminUserName { get; set; }
        public required string SuperAdminPassword { get; set; }
    }
}
