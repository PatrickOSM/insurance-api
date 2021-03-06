using AutoMapper;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Application.Extensions;
using Insurance.Api.Application.Filters;
using Insurance.Api.Application.Interfaces;
using Insurance.Api.Domain.Entities;
using Insurance.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

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

        public async Task<GetInsurancePolicyDto> CreateInsurancePolicy(CreateInsurancePolicyDto insurancePolicy)
        {
            var created = _insurancePolicyRepository.Create(_mapper.Map<InsurancePolicy>(insurancePolicy));
            await _insurancePolicyRepository.SaveChangesAsync();
            return _mapper.Map<GetInsurancePolicyDto>(created);
        }

        public async Task<bool> DeleteInsurancePolicy(Guid id)
        {
            await _insurancePolicyRepository.Delete(id);
            return await _insurancePolicyRepository.SaveChangesAsync() > 0;
        }

        public async Task<PaginatedList<GetInsurancePolicyDto>> GetAllInsurancePolicies(GetInsurancePoliciesFilter filter)
        {

            filter ??= new GetInsurancePoliciesFilter();

            var insurancePolicies = _insurancePolicyRepository
                .GetAll()
                .WhereIf(!string.IsNullOrEmpty(filter.DriversLicence), x => x.DriversLicence == filter.DriversLicence)
                .WhereIf(filter.ExpiredPolicies, x => x.ExpirationDate < DateTime.Now)
                .OrderBy(filter.Ascending ? filter.SortField : filter.SortField + " desc");
            return await _mapper.ProjectTo<GetInsurancePolicyDto>(insurancePolicies).ToPaginatedListAsync(filter.CurrentPage, filter.PageSize);
        }

        public async Task<GetInsurancePolicyDto> GetInsurancePolicyById(Guid id)
        {
            return _mapper.Map<GetInsurancePolicyDto>(await _insurancePolicyRepository.GetById(id));
        }

        public async Task<GetInsurancePolicyDto> GetInsurancePolicyByPolicyId(Guid id, string driversLicense)
        {
            var insurancePolicy = await _insurancePolicyRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id && x.DriversLicence == driversLicense);
            if (insurancePolicy == null)
            {
                return null;
            }

            return _mapper.Map<GetInsurancePolicyDto>(insurancePolicy);
        }

        public Task<GetInsurancePolicyDto> UpdateInsurancePolicy(Guid id, UpdateInsurancePolicyDto updatedInsurancePolicy)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
