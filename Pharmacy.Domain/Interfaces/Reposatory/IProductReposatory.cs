using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Models.PharmacyModels;

namespace Pharmacy.Domain.Interfaces.Reposatory
{
    public interface IProductReposatory :IBaseRepository<Product>
    {
        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId,CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> GetProductsBySupplierAsync(int supplierId, CancellationToken cancellationToken);
        public Task<IEnumerable<Product>> SearchProductsByNameOrPhoneOrEmailAsync(string input, CancellationToken cancellationToken);
    }
}
