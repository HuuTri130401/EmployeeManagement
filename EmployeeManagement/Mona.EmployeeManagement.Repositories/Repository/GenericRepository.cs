using Microsoft.EntityFrameworkCore;
using Mona.EmployeeManagement.Domain.Data;
using Mona.EmployeeManagement.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mona.EmployeeManagement.Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected readonly DbContext _context;

        public GenericRepository(EmployeeContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
