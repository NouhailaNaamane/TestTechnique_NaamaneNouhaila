using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.Repositories.Offer
{
    public class OfferRepository : GenericRepository<Offre>, IOfferRepository
    {
        public OfferRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
