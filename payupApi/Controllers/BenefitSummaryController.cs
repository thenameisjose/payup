using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using payupApi.Models;
using payupApi.Domain;

namespace payupApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitSummaryController : ControllerBase
    {
        IBenefitService _benefitService;

        public BenefitSummaryController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var employees = await _benefitService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(new BenefitSummaryVM() { Id = Guid.NewGuid(), FirstName = "Clark", LastName = "Kent", Dependents = new List<DependentVM>() });
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BenefitSummaryVM value)
        {
            var employee = new Employee()
            {
                FirstName = value.FirstName,
                LastName = value.LastName
            };
            employee.Dependents = value.Dependents.Select(d => new Dependent()
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Relationship = d.Relationship
            }).ToList();
            await _benefitService.AddEmployeeAsync(employee);

            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] BenefitSummaryVM value)
        {
            var employee = new Employee()
            {
                Id = value.Id.GetValueOrDefault(),
                FirstName = value.FirstName,
                LastName = value.LastName
            };
            employee.Dependents = value.Dependents.Select(d => new Dependent()
            {
                EmployeeId = employee.Id,
                Employee = employee,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Relationship = d.Relationship
            }).ToList();
            await _benefitService.UpdateAsync(employee);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _benefitService.DeleteAsync(id);
            return Ok();
        }
    }
}
