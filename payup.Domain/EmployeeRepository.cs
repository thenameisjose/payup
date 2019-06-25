using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace payupApi.Domain
{
    public class EmployeeRepository : IEmployeeRepository
    {
        AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> ListAsync()
        {
           return await _context.Employees
                .Include(e => e.Dependents)
                .ToListAsync();            
        }

        public async Task<Employee> GetAsync(Guid employeeId)
        {
            return await _context.Employees
                .Include(e => e.Dependents)
                .FirstOrDefaultAsync(x => x.Id == employeeId);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<int> DeleteAsync(Guid employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            var result = -1;
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }


        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);

            await _context.SaveChangesAsync();
        }
    }
}
