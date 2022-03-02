using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.Application.Filters
{
    public class GetInsurancePoliciesFilter : PaginationInfoFilter
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "DriversLicence is required.")]
        public string DriversLicence { get; set; }
        public string SortField { get; set; }
        public bool Ascending { get; set; }
        public bool ExpiredPolicies { get; set; }
    }

}
