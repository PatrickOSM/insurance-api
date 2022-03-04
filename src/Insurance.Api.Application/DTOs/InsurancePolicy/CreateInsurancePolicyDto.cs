using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Application.DTOs.InsurancePolicy
{
    public class CreateInsurancePolicyDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "FisrtName is required."), StringLength(254)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName is required."), StringLength(254)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "DriversLicence is required."), StringLength(20)]
        public string DriversLicence { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleName is required."), StringLength(254)]
        public string VehicleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleModel is required."), StringLength(254)]
        public string VehicleModel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleManufacturer is required."), StringLength(254)]
        public string VehicleManufacturer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleYear is required.")]
        public short VehicleYear { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Street is required."), StringLength(254)]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required."), StringLength(254)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "State is required."), StringLength(254)]
        public string State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ZipCode is required."), StringLength(15)]
        public string ZipCode { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Premium is required.")]
        public decimal? Premium { get; set; }
    }
}
