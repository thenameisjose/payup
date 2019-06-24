using System;
using System.Collections.Generic;
namespace payupApi.Models
{
    public class BenefitSummaryVM
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<DependentVM> Dependents { get; set; }
    }
}