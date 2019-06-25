using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace payupApi.Domain
{
    public class BenefitManager : IBenefitManager
    {
        public const decimal EmployeeBenefitsCostPerYear = 1000.00m;
        public const decimal DependentBenefitsCostPerYear = 500.00m;
        public const int PaymentPeriods = 26;
        public const decimal PaymentAmount = 2000.00m;

        public BenefitManager(){}

        private decimal GetBenefitCost(Employee employee)
        {
            decimal cost = 0.0m;
            cost = EmployeeBenefitsCostPerYear;

            cost = ApplyNameDiscount(employee, cost, .9m, "^(a|A)");
            return cost;
        }

        private decimal GetBenefitCost(Dependent dependent)
        {
            decimal cost = 0.0m;
            cost = DependentBenefitsCostPerYear;
            cost = ApplyNameDiscount(dependent, cost, .9m, "^(a|A)");
            return cost;
        }

        public BenefitSummary GetBenefitSummary(Employee employee)
        {
            var summary = new BenefitSummary();
            summary.EmployeeBenefitCost = GetBenefitCost(employee);
            if (employee.Dependents != null)
            {
                summary.DependentBenefitCost = employee.Dependents.Select(d => GetBenefitCost(d)).Sum();
            }
            summary.PaymentAmount = PaymentAmount;
            summary.PaymentPeriods = PaymentPeriods;
            return summary;
        }

        private decimal ApplyNameDiscount(Employee employee, decimal cost, decimal percent, string pattern)
        {
            if (Regex.IsMatch(employee.FirstName, pattern))
                return cost * percent;

            return cost;
        }
        private decimal ApplyNameDiscount(Dependent dependent, decimal cost, decimal percent, string pattern)
        {
            if (Regex.IsMatch(dependent.FirstName, pattern))
                return cost * percent;

            return cost;
        }
    }
}
