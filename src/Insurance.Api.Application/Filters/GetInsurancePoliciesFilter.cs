using System;

namespace Insurance.Api.Application.Filters
{
    public class GetInsurancePoliciesFilter : PaginationInfoFilter
    {
        public string DriversLicence { get; set; }
        public string SortField { get; set; }
        public bool Ascending { get; set; }
        public bool ExpiredPolicies { get; set; }
    }

}
