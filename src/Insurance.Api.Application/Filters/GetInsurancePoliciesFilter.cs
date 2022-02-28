using System;

namespace Insurance.Api.Application.Filters
{
    public class GetInsurancePoliciesFilter : PaginationInfoFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriversLicence { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Premium { get; set; }
    }
}
