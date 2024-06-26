using Mona.EmployeeManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Repositories.RequestFeatures
{
    public static class RepositoryExtensions
    {
        public static IQueryable<Employee> SearchEmployees(this IQueryable<Employee> employees,
             string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(e => e.EmployeeName.ToLower().Contains(lowerCaseTerm));
        }
    }
}
