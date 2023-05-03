using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Repositories.Admin
{
    public class AdminRepository : GenericRepository<Models.Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
