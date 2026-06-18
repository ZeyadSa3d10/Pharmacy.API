using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Infrastructure.Data.Context;

namespace Pharmacy.Infrastructure.Reposatories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        public BaseRepository(AppDbContext db)
        {
            _db = db;
        }
       

        public async Task<T> AddAsync(T entity,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _db.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _db.Set<T>().AddRangeAsync(entities);
            return entities;
        }
        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _db.Set<T>().CountAsync(cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _db.Set<T>().CountAsync(criteria);
        }

        public void Delete(T entity,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _db.Set<T>().Remove(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _db.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Guid id,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _db.Set<T>().FindAsync(id);
        }

        public T Update(T entity,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _db.Set<T>().Update(entity);
            return entity;
        }

    }
}
