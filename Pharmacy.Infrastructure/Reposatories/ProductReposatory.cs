using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Domain.Models.PharmacyModels;
using Pharmacy.Infrastructure.Data.Context;

namespace Pharmacy.Infrastructure.Reposatories
{
    public class ProductReposatory : BaseRepository<Product>, IProductReposatory
    {
        private readonly AppDbContext db;

        public ProductReposatory(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var AllProducts = await db.Products.Where(I => I.CategoryId == categoryId).ToListAsync();
            return AllProducts;
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplierAsync(int supplierId,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var AllProducts = await db.Products.Where(I => I.SupplierId == supplierId).ToListAsync();
            return AllProducts;
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameOrPhoneOrEmailAsync(string input,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var AllProducts = await db.Products.Where(I => I.ProductName == input ||I.Supplier.PhoneNumber==input||I.Supplier.Email==input).ToListAsync();
            return AllProducts;
        }
    }
}
