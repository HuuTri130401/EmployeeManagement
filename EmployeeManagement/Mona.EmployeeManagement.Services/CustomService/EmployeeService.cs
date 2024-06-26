using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mona.EmployeeManagement.Domain.Models;
using Mona.EmployeeManagement.Repositories.RequestFeatures;
using Mona.EmployeeManagement.Repositories.UnitOfWork;
using Mona.EmployeeManagement.Services.Commons;
using Mona.EmployeeManagement.Services.ICustomService;
using Mona.EmployeeManagement.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mona.EmployeeManagement.Domain.Enum.EnumConstants;

namespace Mona.EmployeeManagement.Services.CustomService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, ILogger<EmployeeService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OperationResult<bool>> CreateEmployee(EmployeeRequest employeeRequest, EnumPosition enumPosition)
        {
            var result = new OperationResult<bool>();
            try
            {
                var employee = _mapper.Map<Employee>(employeeRequest);

                var random = new Random();
                int randomId;
                Employee existingEmployee;
                do
                {
                    randomId = random.Next(1, 10000);
                    existingEmployee = await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync($"NV_{DateTime.UtcNow.AddHours(7):yyyy_MM_dd}_{randomId}");
                } while (existingEmployee != null);

                employee.EmployeeId = $"NV_{DateTime.UtcNow.AddHours(7):yyyy_MM_dd}_{randomId}";
                employee.Age = DateTime.UtcNow.AddHours(7).Year - employee.DateOfBirth.Year;
                employee.Position = enumPosition.ToString();

                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var checkResult = _unitOfWork.Save();
                if (checkResult > 0)
                {
                    result.AddResponseStatusCode(StatusCode.Created, "Add Employee Success!", true);
                }
                else
                {
                    result.AddError(StatusCode.BadRequest, "Add Employee Failed!"); ;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in CreateEmployee service method");
                throw;
            }
        }

        public async Task<OperationResult<bool>> DeleteEmployee(string employeeId)
        {
            var result = new OperationResult<bool>();
            try
            {
                var existingEmployee = await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync(employeeId);
                if (existingEmployee != null)
                {
                    _unitOfWork.EmployeeRepository.Remove(existingEmployee);
                    var checkResult = _unitOfWork.Save();
                    if (checkResult > 0)
                    {
                        result.AddResponseStatusCode(StatusCode.Ok, $"Delete Employee have Id: {employeeId} Success.", true);
                    }
                    else
                    {
                        result.AddError(StatusCode.BadRequest, "Delete Employee Failed!"); ;
                    }
                }
                else
                {
                    result.AddResponseStatusCode(StatusCode.NotFound, $"Can't find Employee have Id: {employeeId}. Delete Faild!.", false);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in DeleteEmployee service method, Employee ID: {employeeId}");
                throw;
            }
        }

        public async Task<OperationResult<List<EmployeeResponse>>> GetEmployees(EmployeesParameters employeesParameters)
        {
            var result = new OperationResult<List<EmployeeResponse>>();
            try
            {
                    var employees = await _unitOfWork.EmployeeRepository.GetEmployees(employeesParameters);
                    var employeesResponse = _mapper.Map<List<EmployeeResponse>>(employees);

                if (employeesResponse == null || !employeesResponse.Any())
                {
                    result.AddResponseStatusCode(StatusCode.Ok, $"List Employees Response is Empty!", employeesResponse);
                    return result;
                }
                result.AddResponseStatusCode(StatusCode.Ok, "Get List Employees Response Done.", employeesResponse);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in GetEmployees Service Method");
                throw;
            }
        }

        public async Task<OperationResult<EmployeeResponse>> GetEmployeesById(string employeeId)
        {
            var result = new OperationResult<EmployeeResponse>();
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync(employeeId);
                if (employee == null)
                {
                    result.AddError(StatusCode.NotFound, $"Can't found Employee with Id: {employeeId}");
                    return result;
                }
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                result.AddResponseStatusCode(StatusCode.Ok, $"Get Employee by Id: {employeeId} Success!", employeeResponse);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in GetEmployeesById service method, Employee ID: {employeeId}");
                throw;
            }
        }

        public async Task<OperationResult<bool>> UpdateEmployee(string employeeId, EmployeeRequestUpdate employeeRequestUpdate, EnumPosition enumPosition)
        {
            var result = new OperationResult<bool>();
            try
            {
                var existingEmployee = await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync(employeeId);

                if (existingEmployee != null)
                {
                    bool isAnyFieldUpdated = false;
                    if (employeeRequestUpdate.EmployeeName != null)
                    {
                        existingEmployee.EmployeeName = employeeRequestUpdate.EmployeeName;
                        isAnyFieldUpdated = true;
                    }
                    if (employeeRequestUpdate.DateOfBirth != null)
                    {
                        existingEmployee.DateOfBirth = (DateTime)employeeRequestUpdate.DateOfBirth;
                        existingEmployee.Age = DateTime.UtcNow.AddHours(7).Year - existingEmployee.DateOfBirth.Year;
                        isAnyFieldUpdated = true;
                    }
                    
                    if (enumPosition != null)
                    {
                        existingEmployee.Position = enumPosition.ToString();
                        isAnyFieldUpdated = true;
                    }
                    _unitOfWork.EmployeeRepository.UpdateAsync(existingEmployee);

                    var checkResult = _unitOfWork.Save();
                    if (checkResult > 0)
                    {
                        result.AddResponseStatusCode(StatusCode.NoContent, $"Update Employee have Id: {employeeId} Success.", true);
                    }
                    else
                    {
                        result.AddError(StatusCode.BadRequest, "Update Employee Failed!");
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in UpdateEmployee service method, Employee ID: {employeeId}");
                throw;
            }
        }
    }
}
