using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Infrastructure.Data.Context;

namespace Pharmacy.Infrastructure.Reposatories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly AppDbContext appDbContext;
        private PasswordResetCodeGenerateReposatory? _PasswordResetCodeGenerateReposatory;
        private UserReposatory? _userReposatory;
        private ProductReposatory? _productReposatory;
        private CategoryReposatory? _categoryReposatory;
        private SupplierReposatory? _supplierReposatory;
        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IPasswordResetCodeGenerateReposatory PasswordResetCodeGenerateReposatory => _PasswordResetCodeGenerateReposatory ?? new PasswordResetCodeGenerateReposatory(appDbContext);
        public IUserReposatory UserReposatory =>_userReposatory ?? new UserReposatory(appDbContext);

        public IProductReposatory productReposatory => _productReposatory ?? new ProductReposatory(appDbContext);
        public ISupplierReposatory supplierReposatory => _supplierReposatory ?? new SupplierReposatory(appDbContext);
        public ICategoryReposatory categoryReposatory => _categoryReposatory ?? new CategoryReposatory(appDbContext);


        public void Dispose()
        {
            appDbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await appDbContext.SaveChangesAsync(cancellationToken);
        }

       


    }
}
