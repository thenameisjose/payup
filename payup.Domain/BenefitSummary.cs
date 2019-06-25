using System;
using System.Collections.Generic;
namespace payupApi.Domain
{
    public class BenefitSummary
    {
        public BenefitSummary() { }
        public decimal EmployeeBenefitCost { get; set; }
        public decimal DependentBenefitCost { get; set; }
        public int PaymentPeriods { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}