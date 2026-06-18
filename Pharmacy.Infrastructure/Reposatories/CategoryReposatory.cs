using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Domain.Models.PharmacyModels;
using Pharmacy.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Infrastructure.Reposatories
{
    public class CategoryReposatory : BaseRepository<Category>, ICategoryReposatory
    {
        private readonly AppDbContext db;

        public CategoryReposatory(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<Category> GetCategoryAsync(string Input, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();
            return await db.Categories.Where(s => s.CategoryName == Input || s.Description == Input)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Category> GetCategoryAsync(int Id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await db.Categories.Where(s => s.Id == Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
