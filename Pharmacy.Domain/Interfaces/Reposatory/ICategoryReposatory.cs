using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Models.PharmacyModels;

namespace Pharmacy.Domain.Interfaces.Reposatory
{
    public interface ICategoryReposatory :IBaseRepository<Category>
    {
        public Task<Category> GetCategoryAsync(string Input, CancellationToken cancellationToken);
        public Task<Category> GetCategoryAsync(int ID, CancellationToken cancellationToken);
    }
}
