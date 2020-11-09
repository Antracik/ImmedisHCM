using AutoMapper;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Services.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Services.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserServiceModel, WebUser>().ReverseMap();
        }
    }
}
