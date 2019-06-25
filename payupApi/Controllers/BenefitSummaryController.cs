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
        IEmployeeRepository _employeeRepository;
        IBenefitManager _benefitManager;

        public BenefitSummaryController(IEmployeeRepository employeeRepository, IBenefitManager benefitManager)
        {
            _employeeRepository = employeeRepository;
            _benefitManager = benefitManager;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var employees = await _employeeRepository.ListAsync();
            var models = employees.Select(e => MapToSummaryVM(e));
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            return Ok(MapToSummaryVM(employee));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BenefitSummaryVM summary)
        {
            var employee = new Employee()
            {
                FirstName = summary.FirstName,
                LastName = summary.LastName
            };
            employee.Dependents = summary.Dependents.Select(d => new Dependent()
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Relationship = d.Relationship
            }).ToList();
            await _employeeRepository.AddAsync(employee);


            return Ok(MapToSummaryVM(employee));
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] BenefitSummaryVM summary)
        {
            var employee = new Employee()
            {
                Id = summary.Id.GetValueOrDefault(),
                FirstName = summary.FirstName,
                LastName = summary.LastName
            };
            employee.Dependents = summary.Dependents.Select(d => new Dependent()
            {
                Id = d.Id.GetValueOrDefault(),
                EmployeeId = employee.Id,
                Employee = employee,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Relationship = d.Relationship
            }).ToList();
            await _employeeRepository.UpdateAsync(employee);

            return Ok(MapToSummaryVM(employee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
            return Ok();
        }

        private BenefitSummaryVM MapToSummaryVM(Employee employee)
        {
            return new BenefitSummaryVM()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Dependents = employee.Dependents.Select(d => MapToDependencyVM(d)).ToList(),
                BenefitSummary = _benefitManager.GetBenefitSummary(employee)
            };


        }

        private DependentVM MapToDependencyVM(Dependent dependent)
        {
            return new DependentVM()
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship
            };
        }
    }
}
