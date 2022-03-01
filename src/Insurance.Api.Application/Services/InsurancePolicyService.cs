using System;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Application.Filters;
using Insurance.Api.Application.Interfaces;
using Insurance.Api.Domain.Repositories;

namespace Insurance.Api.Application.Services
{
    public class InsurancePolicyService : IInsurancePolicyService
    {
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;

        private readonly IMapper _mapper;

        public InsurancePolicyService(IMapper mapper, IInsurancePolicyRepository insurancePolicyRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _insurancePolicyRepository = insurancePolicyRepository ?? throw new ArgumentNullException(nameof(insurancePolicyRepository));
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _insurancePolicyRepository.Dispose();
            }
        }

        #region Insurance Policy

        public Task<GetInsurancePolicyDto> CreateInsurancePolicy(CreateInsurancePolicyDto insurancePolicy)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInsurancePolicy(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<GetInsurancePolicyDto>> GetAllInsurancePolicies(GetInsurancePoliciesFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<GetInsurancePolicyDto> GetInsurancePolicyById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetInsurancePolicyDto> UpdateInsurancePolicy(Guid id, UpdateInsurancePolicyDto updatedInsurancePolicy)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
