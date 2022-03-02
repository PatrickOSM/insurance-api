using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Application.DTOs.InsurancePolicy
{
    public class CreateInsurancePolicyDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "FisrtName is required.")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "DriversLicence is required.")]
        public string DriversLicence { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleName is required.")]
        public string VehicleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleModel is required.")]
        public string VehicleModel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleManufacturer is required.")]
        public string VehicleManufacturer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleYear is required.")]
        public short VehicleYear { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Street is required.")]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ZipCode is required.")]
        public string ZipCode { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Premium { get; set; }
    }
}
