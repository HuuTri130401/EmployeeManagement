using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mona.EmployeeManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Domain.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() { }
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options){}
        public DbSet<Employee> Employees { get; set; }
    }
}
