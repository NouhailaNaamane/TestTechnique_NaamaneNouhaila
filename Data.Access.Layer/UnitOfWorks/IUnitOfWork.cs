using Data.Access.Layer.Repositories.Admin;
using Data.Access.Layer.Repositories.Candidature;
using Data.Access.Layer.Repositories.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IAdminRepository AdminRepository { get; init; }
        IOfferRepository OfferRepository { get; init; }
        ICandidatureRepository CandidatureRepository { get; init; }

        Task<int> Save();
    }
}
