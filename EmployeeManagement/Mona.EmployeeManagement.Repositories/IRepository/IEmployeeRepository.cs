using Mona.EmployeeManagement.Domain.Models;
using Mona.EmployeeManagement.Repositories.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Repositories.IRepository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetEmployees(EmployeesParameters employeesParameters);
        Task<Employee> GetEmployeeByEmployeeIdAsync(string employeeId);
    }
}
