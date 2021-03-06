﻿using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class SalaryHistoryMapping : Profile
    {
        public SalaryHistoryMapping()
        {
            CreateMap<SalaryHistory, SalaryHistoryServiceModel>().ReverseMap();
        }
    }
}
