using System;

namespace Insurance.Api.Application.DTOs.InsurancePolicy
{
    public class GetInsurancePolicyDto
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DriversLicence { get; set; }

        public string VehicleName { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleManufacturer { get; set; }

        public short VehicleYear { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Premium { get; set; }
    }
}
