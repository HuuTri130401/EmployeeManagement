using Mona.EmployeeManagement.Repositories.RequestFeatures;
using Mona.EmployeeManagement.Services.Commons;
using Mona.EmployeeManagement.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mona.EmployeeManagement.Domain.Enum.EnumConstants;

namespace Mona.EmployeeManagement.Services.ICustomService
{
    public interface IEmployeeService
    {
        Task<OperationResult<List<EmployeeResponse>>> GetEmployees(EmployeesParameters employeesParameters);
        Task<OperationResult<EmployeeResponse>> GetEmployeesById(string employeeId);
        Task<OperationResult<bool>> CreateEmployee(EmployeeRequest employeeRequest, EnumPosition enumPosition);
        Task<OperationResult<bool>> DeleteEmployee(string employeeId);
        Task<OperationResult<bool>> UpdateEmployee(string employeeId, EmployeeRequestUpdate employeeRequestUpdate, EnumPosition enumPosition);
    }
}
