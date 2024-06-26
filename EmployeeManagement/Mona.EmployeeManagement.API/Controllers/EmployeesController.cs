using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mona.EmployeeManagement.Repositories.RequestFeatures;
using Mona.EmployeeManagement.Services.ICustomService;
using Mona.EmployeeManagement.Services.ViewModel;
using Swashbuckle.AspNetCore.Annotations;
using static Mona.EmployeeManagement.Domain.Enum.EnumConstants;

namespace Mona.EmployeeManagement.API.Controllers
{
    [ApiController]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [SwaggerOperation(Summary = "Get All Employees - {Huu Tri}")]
        [HttpGet("employees")]
        public async Task<IActionResult> GetAllCategories([FromQuery] EmployeesParameters employeesParameters)
        {
            var response = await _employeeService.GetEmployees(employeesParameters);
            return response.IsError ? HandleErrorResponse(response.Errors) : Ok(response);
        }

        [SwaggerOperation(Summary = "Get Employee By Id - {Huu Tri}")]
        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var response = await _employeeService.GetEmployeesById(id);
            return response.IsError ? HandleErrorResponse(response.Errors) : Ok(response);
        }

        [SwaggerOperation(Summary = "Create Employee - {Huu Tri}")]
        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmplopyee(EmployeeRequest employeeRequest, EnumPosition enumPosition)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.CreateEmployee(employeeRequest, enumPosition);
                return response.IsError ? HandleErrorResponse(response.Errors) : Ok(response);
            }
            return BadRequest("Model is invalid");
        }

        [SwaggerOperation(Summary = "Update Employee - {Huu Tri}")]
        [HttpPut("employee/{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeRequestUpdate employeeRequestUpdate, EnumPosition enumPosition)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.UpdateEmployee(id, employeeRequestUpdate, enumPosition);
                return response.IsError ? HandleErrorResponse(response.Errors) : Ok(response);
            }
            return BadRequest("Model is invalid");
        }

        [SwaggerOperation(Summary = "Delete Employee - {Huu Tri}")]
        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var response = await _employeeService.DeleteEmployee(id);
            return response.IsError ? HandleErrorResponse(response.Errors) : Ok(response);
        }
    }
}
