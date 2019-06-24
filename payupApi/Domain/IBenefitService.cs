using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace payupApi.Domain
{
    public interface IBenefitService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Employee employee);
    }
}
