using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Repositories.Admin
{
    public interface IAdminRepository : IGenericRepository<Models.Admin>
    {
        Data.Access.Layer.Models.Admin? GetAdminByLogin(string Login);
    }
}
