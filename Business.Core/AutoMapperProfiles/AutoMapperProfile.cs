using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Access.Layer.Models;
using Data.Transfer.Object.AdministrationDTOs;

namespace Business.Services.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Admin, AdminDTO>();
            // TODO: Add mapping for the offer and the candidature
        }

    }
}
