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

            CreateMap<Users, UserDto>()
                .ForMember(dest => dest.Password, opt => {
                    opt.MapFrom(src => string.Empty);
                });

            CreateMap<UserGroups, UserGroupDto>();

            CreateMap<UserGroupAccess, UserGroupAccessDto>();
        }
    }
}