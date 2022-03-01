using System;
using System.Threading.Tasks;
using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Application.Filters;
using Insurance.Api.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly IInsurancePolicyService _insurancePolicyService;

        public InsurancePolicyController(IInsurancePolicyService insurancePolicyService)
        {
            _insurancePolicyService = insurancePolicyService;
        }


        /// <summary>
        /// Returns all insurance policies in the database
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<GetInsurancePolicyDto>>> GetInsurancePolicies([FromQuery] GetInsurancePoliciesFilter filter)
        {
            return Ok(await _insurancePolicyService.GetAllInsurancePolicies(filter));
        }


        /// <summary>
        /// Get one insurance policy by id from the database
        /// </summary>
        /// <param name="id">The insurance's ID</param>
        /// <returns></returns>
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
        /// <param name="dto">The insurance information</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GetInsurancePolicyDto>> Create([FromBody] CreateInsurancePolicyDto dto)
        {
            GetInsurancePolicyDto newInsurancePolicy = await _insurancePolicyService.CreateInsurancePolicy(dto);
            return CreatedAtAction(nameof(GetInsurancePolicyById), new { id = newInsurancePolicy.Id }, newInsurancePolicy);

        }

        /// <summary>
        /// Update a insurance policy from the database
        /// </summary>
        /// <param name="id">The insurance's ID</param>
        /// <param name="dto">The update object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GetInsurancePolicyDto>> Update(Guid id, [FromBody] UpdateInsurancePolicyDto dto)
        {

            GetInsurancePolicyDto updatedInsurancePolicy = await _insurancePolicyService.UpdateInsurancePolicy(id, dto);

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
