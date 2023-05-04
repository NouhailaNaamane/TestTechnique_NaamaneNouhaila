using AutoMapper;
using Business.Interfaces.Admin;
using Data.Access.Layer.UnitOfWorks;
using Data.Transfer.Object.Administration;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public AdminDTO GetAdminByLogin(string Login)
        {
            return _autoMapper.Map<AdminDTO>(this._unitOfWork.AdminRepository.GetAdminByLogin(Login));
        }
    }
}
