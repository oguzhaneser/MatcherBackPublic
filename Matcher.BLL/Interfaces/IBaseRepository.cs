using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Matcher.BLL.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
                                          Expression<Func<TEntity, TEntity>> select = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          int pageSize = 0,
                                          int pageNumber = 0);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
                                          Expression<Func<TEntity, TEntity>> select = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          int pageSize = 0,
                                          int pageNumber = 0);

        TEntity Get(Expression<Func<TEntity, bool>> filter = null,
                          Expression<Func<TEntity, TEntity>> select = null,
                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool showDeleted = false);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                          Expression<Func<TEntity, TEntity>> select = null,
                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool showDeleted = false);

        TEntity GetByID(int id);
        Task<TEntity> GetByIDAsync(int id);

        Task Insert(TEntity entity);
        Task Insert(IEnumerable<TEntity> entities);

        Task Update(TEntity entity);
        Task Update(IEnumerable<TEntity> entities);

        Task Delete(TEntity entity);
        Task Delete(IEnumerable<TEntity> entities);
    }
}
