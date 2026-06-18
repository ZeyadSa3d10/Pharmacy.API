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
    public class SupplierReposatory : BaseRepository<Supplier>, ISupplierReposatory
    {
        private readonly AppDbContext db;

        public SupplierReposatory(AppDbContext db) : base(db)
        {
            this.db = db;
        }
        public async Task<Supplier> GetSupplierAsync(string Input, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await db.Suppliers.Where(s => s.SupplierName == Input || s.Email == Input || s.PhoneNumber == Input||s.NationalId==Input)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Supplier> GetSupplierAsync(int Id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await db.Suppliers.Where(s => s.Id ==Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
