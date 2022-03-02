using System;
using System.Threading.Tasks;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Application.Filters;
using Insurance.Api.Application.Interfaces;
using Insurance.Api.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly IInsurancePolicyService _insurancePolicyService;

        public InsurancePolicyController(IInsurancePolicyService insurancePolicyService)
        {
            _insurancePolicyService = insurancePolicyService;
        }


        /// <summary>
        /// Returns a list of insurance policies with the possibility to filter it
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>A list of insurance policies</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /InsurancePolicy
        ///     {
        ///        "driversLicence": "749882582"
        ///        "sortField": "VehicleYear"
        ///        "ascending": true
        ///        "expiredPolicies": true
        ///     }
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<GetInsurancePolicyDto>>> GetInsurancePolicies([FromQuery] GetInsurancePoliciesFilter filter)
        {
            return Ok(await _insurancePolicyService.GetAllInsurancePolicies(filter));
        }

        /// <summary>
        /// Get one insurance policy by id from the database
        /// </summary>
        /// <param name="id">The policy ID</param>
        /// <param name="driversLicense">Drivers license #</param>
        /// <returns>The insurance policy with mathing ID and Driver's License #</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /InsurancePolicy/687d9fd5-2752-4a96-93d5-0f33a49913c6/749882582
        ///
        /// </remarks>
        [HttpGet]
        [Route("{id}/{driversLicense}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetInsurancePolicyDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetInsurancePolicyDto>> GetInsurancePolicyByPolicyId(Guid id, string driversLicense)
        {
            GetInsurancePolicyDto insurancePolicy = await _insurancePolicyService.GetInsurancePolicyByPolicyId(id, driversLicense);
            if (insurancePolicy == null)
            {
                return NotFound();
            }

            return Ok(insurancePolicy);
        }

        /// <summary>
        /// Get one insurance policy by id from the database
        /// </summary>
        /// <param name="id">The insurance's ID</param>
        /// <returns>The insurance policy with mathing ID and Driver's License #</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetInsurancePolicyDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetInsurancePolicyDto>> GetInsurancePolicyById(Guid id)
        {
            GetInsurancePolicyDto insurancePolicy = await _insurancePolicyService.GetInsurancePolicyById(id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }

            return Ok(insurancePolicy);
        }

        /// <summary>
        /// Insert one insurance policy in the database
        /// </summary>
        /// <param name="insurancePolicy">The insurance information</param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult<GetInsurancePolicyDto>> Create([FromBody] CreateInsurancePolicyDto insurancePolicy)
        {
            GetInsurancePolicyDto newInsurancePolicy = await _insurancePolicyService.CreateInsurancePolicy(insurancePolicy);
            return CreatedAtAction(nameof(GetInsurancePolicyById), new { id = newInsurancePolicy.Id }, newInsurancePolicy);

        }

        /// <summary>
        /// Update a insurance policy from the database
        /// </summary>
        /// <param name="id">The insurance's ID</param>
        /// <param name="insurancePolicy">The update object</param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetInsurancePolicyDto>> Update(Guid id, [FromBody] UpdateInsurancePolicyDto insurancePolicy)
        {

            GetInsurancePolicyDto updatedInsurancePolicy = await _insurancePolicyService.UpdateInsurancePolicy(id, insurancePolicy);

            if (updatedInsurancePolicy == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        /// <summary>
        /// Delete a insurance policy from the database
        /// </summary>
        /// <param name="id">The insurance's ID</param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool deleted = await _insurancePolicyService.DeleteInsurancePolicy(id);
            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
