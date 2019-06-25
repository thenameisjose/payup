using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace payupApi.Domain
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> ListAsync();

        Task<Employee> GetAsync(Guid employeeId);

        Task<Employee> AddAsync(Employee employee);

        Task<int> DeleteAsync(Guid employeeId);

        Task UpdateAsync(Employee employee);
    }
}