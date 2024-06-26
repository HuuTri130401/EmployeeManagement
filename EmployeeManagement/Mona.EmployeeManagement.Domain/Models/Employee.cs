using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Domain.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
    }
}
