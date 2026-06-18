using Pharmacy.Domain.ApplicationDtos.ProductDtos;
using Pharmacy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces.Service
{
    public interface IUserService
    {
        public Task<ApiResponse<SupplierDto>> AddSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken);
    }
}
