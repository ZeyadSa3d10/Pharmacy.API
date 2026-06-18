using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Interfaces.Reposatory;

namespace Pharmacy.Domain.Interfaces.Service
{
    public interface IUnitOfWork
    {
        IPasswordResetCodeGenerateReposatory PasswordResetCodeGenerateReposatory { get; }
        ICategoryReposatory categoryReposatory { get; }
        ISupplierReposatory supplierReposatory { get; }
        IProductReposatory productReposatory { get; }
        IUserReposatory UserReposatory { get; }
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
