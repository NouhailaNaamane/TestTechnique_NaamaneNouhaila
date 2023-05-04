using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Admin;
using Data.Access.Layer.Repositories.Candidature;
using Data.Access.Layer.Repositories.Offer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Layer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAdminRepository AdminRepository { get; init; }
        public IOfferRepository OfferRepository { get; init; }
        public ICandidatureRepository CandidatureRepository { get; init; }
        private readonly ApplicationContext DbContext;

        public UnitOfWork(IAdminRepository adminRepository, IOfferRepository offerRepository, ICandidatureRepository candidatureRepository, ApplicationContext dbContext)
        {
            AdminRepository = adminRepository;
            OfferRepository = offerRepository;
            CandidatureRepository = candidatureRepository;
            DbContext = dbContext;
        }

       
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
        }

        public async Task<int> Save()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
