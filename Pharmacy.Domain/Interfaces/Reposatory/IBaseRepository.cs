using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces.Reposatory
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        Task<T> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities,CancellationToken cancellationToken);
        T Update(T entity, CancellationToken cancellationToken);
        void Delete(T entity,CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<int> CountAsync(Expression<Func<T, bool>> criteria,CancellationToken cancellationToken);
    }
}