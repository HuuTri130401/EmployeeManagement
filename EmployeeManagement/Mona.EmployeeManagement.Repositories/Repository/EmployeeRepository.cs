using Microsoft.EntityFrameworkCore;
using Mona.EmployeeManagement.Domain.Data;
using Mona.EmployeeManagement.Domain.Models;
using Mona.EmployeeManagement.Repositories.IRepository;
using Mona.EmployeeManagement.Repositories.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Repositories.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext context) : base(context)
        {
        }

        public async Task<Employee> GetEmployeeByEmployeeIdAsync(string employeeId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(pi => pi.EmployeeId.Equals(employeeId));
        }

        public async Task<int> GetEmployeeCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<List<Employee>> GetEmployees(EmployeesParameters employeesParameters)
        {
            var employees = await _dbSet
                .SearchEmployees(employeesParameters.SearchTerm)
                .ToListAsync();
            var count = _dbSet.Count();
            return PagedList<Employee>
                .ToPagedList(employees, count, employeesParameters.PageNumber, employeesParameters.PageSize);
        }

        //public Task<Employee> CreateEmployee(Employee employee)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteEmployee(Guid employeeId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SoftDeleteEmployee(Guid employeeId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Employee> UpdateEmployee(Employee employee)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
