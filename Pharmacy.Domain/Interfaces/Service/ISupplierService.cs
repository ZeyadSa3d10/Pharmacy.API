using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Domain.Interfaces.Service
{
    public interface ISupplierService
    {
        public Task<ApiResponse<SupplierDto>> AddSupplierAsync(SupplierDto supplierDto,CancellationToken cancellationToken);
        public Task<ApiResponse<bool>> DeleteSupplier(string NationalId, CancellationToken cancellationToken);
        public Task<ApiResponse<SupplierDto>> UpdateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken);
        public Task<ApiResponse<IEnumerable<SupplierDto>>> GetAllSuppliersAsync(CancellationToken cancellationToken);
        public Task<ApiResponse<SupplierDto>> GetSupplierAsync(string input, CancellationToken cancellationToken);
        public Task<ApiResponse<SupplierDto>> GetSupplierAsync(int Id, CancellationToken cancellationToken);
    }
}
