using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Models.PharmacyModels;

namespace Pharmacy.Domain.Interfaces.Reposatory
{
    public interface ISupplierReposatory : IBaseRepository<Supplier>
    {
        public Task<Supplier> GetSupplierAsync(string Input, CancellationToken cancellationToken);
        public Task<Supplier> GetSupplierAsync(int ID, CancellationToken cancellationToken);
    }
}
