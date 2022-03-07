using AutoMapper;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Domain.Entities;

namespace Insurance.Api.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InsurancePolicy, GetInsurancePolicyDto>().ReverseMap();
            CreateMap<CreateInsurancePolicyDto, InsurancePolicy>();
            CreateMap<UpdateInsurancePolicyDto, InsurancePolicy>();
        }
    }
}
