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
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        //Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        //                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //                                string includeString = null,
        //                                bool disableTracking = true);
        //Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        //                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //                                List<Expression<Func<T, object>>> includes = null,
        //                                bool disableTracking = true);
        Task<T> GetByIdAsync(Guid? id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(Guid? id);
    }
}
