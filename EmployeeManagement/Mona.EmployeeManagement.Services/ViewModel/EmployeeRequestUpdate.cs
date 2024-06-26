using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mona.EmployeeManagement.Domain.Enum.EnumConstants;

namespace Mona.EmployeeManagement.Services.ViewModel
{
    public class EmployeeRequestUpdate
    {
        [StringLength(50, ErrorMessage = "EmployeeName length cannot exceed 50 characters")]
        [MinLength(1, ErrorMessage = "EmployeeName length must be at least 1 characters")]
        public string? EmployeeName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
