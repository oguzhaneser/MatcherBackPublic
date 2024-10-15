using Matcher.BLL.Interfaces;
using Matcher.DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Matcher.BLL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private MatcherDBContext _context;
        protected IQueryable<User> _query;
        public UserRepository()
        {
            _context = new();
            _query = _context.Set<User>();
        }

        public async Task Delete(User entity)
        {
            entity.IsDeleted = true;
            await Update(entity);
        }

        public async Task Delete(IEnumerable<User> entities)
        {
            entities.ToList().ForEach(user => user.IsDeleted = true);
            await Update(entities);
        }

        public User Get(Expression<Func<User, bool>> filter = null, Expression<Func<User, User>> select = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null, bool showDeleted = false)
        {
            if (filter != null)
                _query = _query.Where(filter);
            if (select != null)
                _query = _query.Select(select);
            if (orderBy != null)
                _query = orderBy(_query);

            if (include != null)
                _query = include(_query);

            return _query.FirstOrDefault();
        }

        public IEnumerable<User> GetAll(Expression<Func<User, bool>> filter = null, Expression<Func<User, User>> select = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null, int pageSize = 0, int pageNumber = 0)
        {
            if (filter != null)
                _query = _query.Where(filter);
            if (select != null)
                _query = _query.Select(select);

            else if (orderBy != null)
                _query = orderBy(_query);

            if (pageSize > 0 && pageNumber > 0)
                _query = _query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (include != null)
                _query = include(_query);

            return _query.ToList();
        }

        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter = null, Expression<Func<User, User>> select = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null, int pageSize = 0, int pageNumber = 0)
        {
            if (filter != null)
                _query = _query.Where(filter);

            if (select != null)
                _query = _query.Select(select);

            else if (orderBy != null)
                _query = orderBy(_query);

            if (pageSize > 0 && pageNumber > 0)
                _query = _query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (include != null)
                _query = include(_query);

            return await _query.ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter = null, Expression<Func<User, User>> select = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, Func<IQueryable<User>, IIncludableQueryable<User, object>> include = null, bool showDeleted = false)
        {
            if (filter != null)
                _query = _query.Where(filter);
            if (select != null)
                _query = _query.Select(select);
            if (orderBy != null)
                _query = orderBy(_query);
            if (include != null)
                _query = include(_query);

            return await _query.FirstOrDefaultAsync();
        }

        public User GetByID(int id)
        {
            return _query.FirstOrDefault(x=>x.Id == id);
        }

        public async Task<User> GetByIDAsync(int id)
        {
            return await _query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Insert(IEnumerable<User> entities)
        {
            await _context.Set<User>().AddRangeAsync(entities);
        }

        public async Task Update(User entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _context.Set<User>().Update(entity);
        }

        public async Task Update(IEnumerable<User> entities)
        {
            entities.ToList().ForEach(user => user.ModifiedDate = DateTime.Now);
            _context.Set<User>().UpdateRange(entities);
        }
    }
}
