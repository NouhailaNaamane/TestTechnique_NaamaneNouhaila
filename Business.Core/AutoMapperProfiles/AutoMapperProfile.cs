﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Access.Layer.Models;
using Data.Transfer.Object.Administration;

namespace Business.Services.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Data.Access.Layer.Models.Admin, AdminDTO>();
            // TODO: Add mapping for the offer and the candidature
        }

    }
}
