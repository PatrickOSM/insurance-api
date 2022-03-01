using System;
using System.Threading.Tasks;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Application.Filters;

namespace Insurance.Api.Application.Interfaces
{
    public interface IInsurancePolicyService : IDisposable
    {
        #region InsurancePolicy Methods

        public Task<PaginatedList<GetInsurancePolicyDto>> GetAllInsurancePolicies(GetInsurancePoliciesFilter filter);

        public Task<GetInsurancePolicyDto> GetInsurancePolicyById(Guid id);

        public Task<GetInsurancePolicyDto> CreateInsurancePolicy(CreateInsurancePolicyDto insurancePolicy);

        public Task<GetInsurancePolicyDto> UpdateInsurancePolicy(Guid id, UpdateInsurancePolicyDto updatedInsurancePolicy);

        public Task<bool> DeleteInsurancePolicy(Guid id);

        #endregion
    }
}