using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Repositories.Candidature
{
    public class CandidatureRepository : GenericRepository<Models.Candidature>, ICandidatureRepository
    {
        public CandidatureRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
