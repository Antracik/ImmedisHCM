using AutoMapper;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Services.Models.Identity;

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
