using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Services.ViewModel
{
    public class EmployeeResponse
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
    }
}
