using Data.Transfer.Object.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Admin
{
    public interface IAdminService
    {
        AdminDTO GetAdminByLogin(string login);
    }
}
