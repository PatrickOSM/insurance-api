using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Application.Filters
{
    public class GetInsurancePoliciesFilter : PaginationInfoFilter
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "DriversLicence is required.")]
        public string DriversLicence { get; set; }
        public string SortField { get; set; } = "VehicleYear"; // Default filter for sort field
        public bool Ascending { get; set; }
        public bool ExpiredPolicies { get; set; }
    }

}
