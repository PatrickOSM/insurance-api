using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.InsurancePolicy;
using Insurance.Api.Application.Filters;
using Insurance.Api.Application.Interfaces;
using Insurance.Api.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Polly;
using SmartyStreets;
using SmartyStreets.USStreetApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly IInsurancePolicyService _insurancePolicyService;
        private readonly IConfiguration _configuration;

        public InsurancePolicyController(IInsurancePolicyService insurancePolicyService, IConfiguration configuration)
        {
            _insurancePolicyService = insurancePolicyService;
            _configuration = configuration;
        }


        /// <summary>
        /// Returns a list of insurance policies with the possibility to filter it
        /// </summary>
        /// <param name="filter">Object filter to get the results</param>
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
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /InsurancePolicy
        ///     {
        ///         "firstName": "Timothy",
        ///         "lastName": "Davis",
        ///         "driversLicence": "594533707",
        ///         "vehicleName": "Peugeot 406",
        ///         "vehicleModel": "Peugeot Coupé",
        ///         "vehicleManufacturer": "Peugeot",
        ///         "vehicleYear": 1995,
        ///         "street": "1000 Radio Park Drive",
        ///         "city": "Augusta",
        ///         "state": "GA",
        ///         "zipCode": "30904",
        ///         "effectiveDate": "2022-04-10T00:00:00.0000000-00:00",
        ///         "expirationDate": "2022-04-10T00:00:00.0000000-00:00",
        ///         "premium": 500000
        ///     }
        ///
        /// </remarks>
        //[Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult<GetInsurancePolicyDto>> Create([FromBody] CreateInsurancePolicyDto insurancePolicy)
        {
            string authId = _configuration.GetValue<string>("SmartyCredentials:AuthId");
            string authToken = _configuration.GetValue<string>("SmartyCredentials:AuthToken");

            // Insurance effective date must be at least 30 days in the future from the creation date
            if (insurancePolicy.EffectiveDate > DateTime.Now.AddDays(30))
            {
                // Validate if the vehicle meet the classic status
                if (insurancePolicy.VehicleYear < 1998)
                {
                    // Create an API call to validate if the informed address is valid
                    var client = new ClientBuilder(authId, authToken).WithLicense(new List<string> { "us-core-cloud" }).BuildUsStreetApiClient();

                    var lookup = new Lookup
                    {
                        Street = insurancePolicy.Street,
                        City = insurancePolicy.City,
                        State = insurancePolicy.State,
                        ZipCode = insurancePolicy.ZipCode,
                        MaxCandidates = 1,
                        MatchStrategy = Lookup.ENHANCED
                    };

                    // Send an API call to validate the address
                    try
                    {
                        client.Send(lookup);
                    }
                    catch (SmartyException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    // True when a valid US address is informed
                    if (lookup.Result.Count > 0)
                    {
                        // Verify if the state regulation will allow this insurance to be created
                        var stateRegulation = ValidateStateRegulation(insurancePolicy);

                        if (stateRegulation.Successful)
                        {
                            var maxRetryAttempts = 3;
                            var pauseBetweenFailures = TimeSpan.FromSeconds(2);

                            // Using polly to handle the retry policy for the call, adding a max retry attempt and a timestamp between the calls
                            var retryPolicy = Policy
                                .Handle<HttpRequestException>()
                                .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

                            GetInsurancePolicyDto newInsurancePolicy = await _insurancePolicyService.CreateInsurancePolicy(insurancePolicy);

                            // Call the needed notification services after 
                            await retryPolicy.ExecuteAsync(async () =>
                            {
                                await ServicesCall();
                            });

                            return CreatedAtAction(nameof(GetInsurancePolicyById), new { id = newInsurancePolicy.Id }, newInsurancePolicy);
                        }
                        else
                        {
                            return ValidationProblem("Your request was not accepted due to state regulation");
                        }
                    }
                    else
                    {
                        return NotFound("Informed adreess not found.");
                    }

                }
                else
                {
                    return BadRequest("VehicleYear should be before 1998 to meet the “classic vehicle” status.");
                }
            }
            else
            {
                return BadRequest("EffectiveDate must be at least 30 days in the future from now.");
            }
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

        // A stub version of a class to validate if the state regulation allow for the insurance to be created
        // Disable warning about the unused variable (it's a stub class)
        #pragma warning disable S1172 // Unused method parameters should be removed
        private ValidationResponse ValidateStateRegulation(CreateInsurancePolicyDto createInsurancePolicyDto)
        #pragma warning restore S1172 // Unused method parameters should be removed
        {
            var response = new ValidationResponse();

            int random = new Random().Next(0, 5);

            switch (random)
            {
                case 0:
                    response.Successful = false;
                    response.Information = "State regulation not approved";
                    return response;
                case 1:
                    response.Successful = true;
                    response.Information = "State regulation approved";
                    return response;
            }

            return response;
        }

        static async Task ServicesCall()
        {
            await Task.Delay(2000);
            bool randomBool = new Random().NextDouble() > 0.5;
            if (!randomBool)
            {
                throw new HttpRequestException();
            }

        }


    }

    // Object to mock a random response for the method ValidateStateRegulation
    public class ValidationResponse
    {
        public bool Successful { get; set; }
        public string Information { get; set; }
    }
}
