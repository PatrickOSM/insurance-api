using System;

namespace Insurance.Api.Application.DTOs.InsurancePolicy
{
    public class CreateInsurancePolicyDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriversLicence { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Premium { get; set; }
    }
}
