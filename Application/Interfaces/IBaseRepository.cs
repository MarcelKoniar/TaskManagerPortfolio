using Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Interfaces
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, bool disableTracking = true);
        Task<T> GetByIdAsync(Guid? id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(Guid? id);
    }
}
