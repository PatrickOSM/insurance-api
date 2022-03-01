using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Insurance.Api.Domain.Core.Entities;

namespace Insurance.Api.Domain.Entities
{
    public class InsurancePolicy : Entity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string DriversLicence { get; set; }

        [Required]
        public string VehicleName { get; set; }

        [Required]
        public string VehicleModel { get; set; }

        [Required]
        public string VehicleManufacturer { get; set; }

        [Required]
        public short VehicleYear { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Premium { get; set; }
    }
}
