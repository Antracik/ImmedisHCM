using AutoMapper;
using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Services.Models.Identity;

namespace ImmedisHCM.Web.Models.Mappings
{
    public class AccountManageMaping : Profile
    {
        public AccountManageMaping()
        {
            CreateMap<UserServiceModel, ProfileViewModel>();
            CreateMap<EmployeeServiceModel, ProfileViewModel>()
                .ForMember(x => x.City, opts => opts.MapFrom(x => x.Location.City))
                .ForMember(x => x.Country, opts => opts.MapFrom(x => x.Location.City.Country))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ProfileViewModel, EmployeeServiceModel>()
                .ForMember(x => x.Location, opts => opts.MapFrom(x => x.Location))
                .ForPath(x => x.Location.City, opt =>
                {
                    opt.Condition(
                       s => s.Source.City != null
                    );
                    opt.MapFrom(s => s.City);
                })
                .ForPath(x => x.Location.City.Country, opt =>
                {
                    opt.Condition(
                       s => s.Source.Country != null
                    );
                    opt.MapFrom(s => s.Country);
                });
                

            CreateMap<EmployeeServiceModel, ManagerViewModel>().ReverseMap();

            CreateMap<EmergencyContactViewModel, EmergencyContactServiceModel>()
                .ReverseMap();
        }
    }
}
