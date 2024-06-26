using Microsoft.EntityFrameworkCore;
using Mona.EmployeeManagement.Domain.Data;
using Mona.EmployeeManagement.Repositories.IRepository;
using Mona.EmployeeManagement.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeContext _employeeContext;
        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(
            EmployeeContext employeeContext,
            IEmployeeRepository employeeRepo
            )
        {
            _employeeContext = employeeContext;
            EmployeeRepository = employeeRepo;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _employeeContext.Dispose();
            }
        }
        public int Save()
        {
            return _employeeContext.SaveChanges();
        }
    }
}
