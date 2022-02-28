using AutoMapper;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Application.DTOs.User;
using Insurance.Api.Domain.Auth;
using Insurance.Api.Domain.Entities;

namespace Insurance.Api.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // InsurancePolicy Map
            CreateMap<InsurancePolicy, GetInsurancePolicyDto>().ReverseMap();
            CreateMap<CreateInsurancePolicyDto, InsurancePolicy>();
            CreateMap<UpdateInsurancePolicyDto, InsurancePolicy>();

            // User Map
            CreateMap<User, GetUserDto>().ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(x => x.Role == Roles.Admin)).ReverseMap();
            CreateMap<CreateUserDto, User>().ForMember(dest => dest.Role,
                opt => opt.MapFrom(org => org.IsAdmin ? Roles.Admin : Roles.User));
            CreateMap<UpdatePasswordDto, User>();
        }
    }
}
