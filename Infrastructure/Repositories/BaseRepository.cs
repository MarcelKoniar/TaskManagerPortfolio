using Application.Interfaces;
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly AppDbContext DbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbContext.Database.EnsureCreated();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            var dbContext = DbContext;
            return dbContext.Set<T>().Where(x => !x.Deleted);
        }

        public async Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            var dbContext = DbContext;

            return dbContext.Set<T>().Where(predicate);
        }

        //public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        //{
        //    var dbContext = DbContext;

        //    IQueryable<T> query = dbContext.Set<T>();
        //    if (disableTracking) query = query.AsNoTracking();

        //    if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

        //    if (predicate != null) query = query.Where(predicate);

        //    if (orderBy != null)
        //        return await orderBy(query).ToListAsync();
        //    return await query.ToListAsync();
        //}

        //public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        //{
        //    var dbContext = DbContext;

        //    IQueryable<T> query = dbContext.Set<T>();
        //    if (disableTracking) query = query.AsNoTracking();

        //    if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        //    if (predicate != null) query = query.Where(predicate);

        //    if (orderBy != null)
        //        return await orderBy(query).ToListAsync();
        //    return await query.ToListAsync();
        //}

        public virtual async Task<T> GetByIdAsync(Guid? id)
        {
            var dbContext = DbContext;
            return await dbContext.Set<T>().FirstOrDefaultAsync(x => !x.Deleted && x.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            var dbContext = DbContext;

            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var dbContext = DbContext;

            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid? id)
        {
            var dbContext = DbContext;

            T entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.Deleted = true;
                //DbContext.Set<T>().Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }

    }

}
