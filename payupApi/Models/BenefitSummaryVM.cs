using payupApi.Domain;
using System;
using System.Collections.Generic;
namespace payupApi.Models
{
    public class BenefitSummaryVM
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<DependentVM> Dependents { get; set; }
        public BenefitSummary BenefitSummary { get; set; } 
    }
}