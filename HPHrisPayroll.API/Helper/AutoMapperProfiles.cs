using AutoMapper;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Helper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Companies, CompanyDto>();

            CreateMap<Employees, EmployeeDto>();
            CreateMap<Employees, EmployeeForLookupDto>()
                .ForMember(dest => dest.FullName, opt => {
                    opt.MapFrom(src => src.FirstName + ' ' + src.LastName);
                });
            

            CreateMap<Users, UserDto>()
                .ForMember(dest => dest.Password, opt => {
                    opt.MapFrom(src => string.Empty);
                })
                .ForMember(dest => dest.UserGroupName, opt => {
                    opt.MapFrom(src => src.UserGroup.UserGroupName);
                })
                .ForMember(dest => dest.FullName, opt => {
                    opt.MapFrom(src => src.EmployeeNoNavigation.FirstName + ' ' + src.EmployeeNoNavigation.LastName);
                });

            CreateMap<Users, UserDtoForLoginResponse>()
                .ForMember(dest => dest.FullName, opt => {
                    opt.MapFrom(src => src.EmployeeNoNavigation.FirstName + ' ' + src.EmployeeNoNavigation.LastName);
                });

            CreateMap<UserGroups, UserGroupDto>();

            CreateMap<UserGroupAccess, UserGroupAccessDto>()
                .ForMember(dest => dest.RoleName, opt => {
                    opt.MapFrom(src => src.Role.RoleName);
                });
        }
    }
}