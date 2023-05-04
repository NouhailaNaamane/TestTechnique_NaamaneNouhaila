using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Generic;
using LinqKit;
using Microsoft.EntityFrameworkCore;
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

        public Models.Admin? GetAdminByLogin(string Login)
        {
            var whereBuilder = PredicateBuilder.New<Data.Access.Layer.Models.Admin>(true);

            whereBuilder.Or(A => !string.IsNullOrEmpty(A.UserName) && A.UserName.ToUpper() == Login.ToUpper());
            whereBuilder.Or(A => !string.IsNullOrEmpty(A.Email) && A.Email.ToUpper() == Login.ToUpper());
            whereBuilder.Or(A => !string.IsNullOrEmpty(A.PhoneNumber) && A.PhoneNumber.ToUpper() == Login.ToUpper());
            return _dbContext.Set<Models.Admin>().Where(whereBuilder).FirstOrDefault();

        }
    }
}
