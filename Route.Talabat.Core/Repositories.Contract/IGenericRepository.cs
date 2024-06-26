﻿using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetWithSpecAsync(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications);
        Task<int> GetCountWithSpecAsync(ISpecifications<T> specifications);
    }
}
