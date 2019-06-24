using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace payupApi.Domain
{
    public class BenefitService : IBenefitService
    {
        IEmployeeRepository _employeeRepository;
        IDependentRepository _dependentRepository;
        public BenefitService(IEmployeeRepository employeeRepo, IDependentRepository dependentRepo)
        {
            _employeeRepository = employeeRepo;
            _dependentRepository = dependentRepo;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _employeeRepository.FindAll().ToListAsync();
            return employees;
        }

        public async Task<Employee> AddEmployeeAsync(Employee emp)
        {
            await _employeeRepository.CreateAsync(emp);
            await _employeeRepository.SaveAsync();
            return emp;
        }

        public async Task DeleteAsync(Guid id)
        {
            var employee = await _employeeRepository.FindByCondition(e => e.Id == id).SingleOrDefaultAsync();
            if (null != employee)
            {
                _employeeRepository.Delete(employee);
                await _employeeRepository.SaveAsync();
            }
        }

        public async Task UpdateAsync(Employee employee)
        {

            var deps = await _dependentRepository.FindByCondition(d => d.EmployeeId == employee.Id).ToArrayAsync();
            _dependentRepository.DeleteRange(deps);
            if (null != employee.Dependents)
            {
                await _dependentRepository.AddRangeAsync(employee.Dependents.ToArray());
            }
            await _dependentRepository.SaveAsync();
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync();


        }
    }
}
