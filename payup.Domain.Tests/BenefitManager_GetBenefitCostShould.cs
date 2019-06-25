using NUnit.Framework;
using payupApi.Domain;
using System.Collections.Generic;

namespace payupApi.Domain.Tests
{
    public class BenefitManager_GetBenefitCostShould
    {
        private BenefitManager _benefitManager;
        [SetUp]
        public void Setup()
        {
            _benefitManager = new BenefitManager();
        }

        [Test]
        public void ReturnEmployeeCostPerYear()
        {
            var employee = new Employee()
            {
                FirstName = "Jose",
                LastName = "Cortes"
            };

            var result = _benefitManager.GetBenefitSummary(employee);

            Assert.AreEqual(BenefitManager.EmployeeBenefitsCostPerYear, result.EmployeeBenefitCost);
        }

        [Test]
        public void ReturnDependentCostPerYear()
        {
            var employee = new Employee()
            {
                FirstName = "Jose",
                LastName = "Cortes",
                Dependents = new List<Dependent>()
                {
                    new Dependent(){FirstName = "Dependent", LastName = "One", Relationship = "Spouse"},
                    new Dependent(){FirstName = "Dependent", LastName = "Two", Relationship = "Child"}
                }
            };

            var result = _benefitManager.GetBenefitSummary(employee);

            Assert.AreEqual(BenefitManager.DependentBenefitsCostPerYear * employee.Dependents.Count, result.DependentBenefitCost);
        }

        [Test]
        public void ReturnEmployeeCostWithDiscount()
        {
            var employee = new Employee()
            {
                FirstName = "Alex",
                LastName = "Smith",
                Dependents = new List<Dependent>()
                {
                    new Dependent(){FirstName = "Dependent", LastName = "One", Relationship = "Spouse"},
                    new Dependent(){FirstName = "Dependent", LastName = "Two", Relationship = "Child"}
                }
            };

            var result = _benefitManager.GetBenefitSummary(employee);

            Assert.AreEqual(BenefitManager.EmployeeBenefitsCostPerYear * .9m, result.EmployeeBenefitCost);
        }
    }
}