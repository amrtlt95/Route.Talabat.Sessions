using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().AsNoTracking().ToListAsync();


        public async Task<T?> GetAsync(int id) => await _dbContext.Set<T>().FindAsync(id);
    }
}
