using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.Models.PharmacyModels;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Domain.Interfaces.Service
{
    public interface IProductServices
    {
        public Task<ApiResponse<productDto>> AddProductAsync(productDto productDto,CancellationToken cancellationToken);
        public Task<ApiResponse<IEnumerable<productDto>>> AddRangeProductAsync(IEnumerable<productDto> productDto,CancellationToken cancellationToken);
        public Task<ApiResponse<int>> CountAsync(CancellationToken cancellationToken);
        public Task<ApiResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<ApiResponse<IEnumerable<productDto>>> GetAllProductsAsync(); 
        public Task<ApiResponse<productDto>> GetProductAsync(int productId);
        public Task<Product> Update(productDto dto);
        public Task<ApiResponse<IEnumerable<productDto>>> GetProductsByCategoryAsync(int categoryId);
        public Task<ApiResponse<IEnumerable<productDto>>> GetProductsBySupplierAsync(int SupplierId);
        public Task<ApiResponse<IEnumerable<productDto>>> SearchProductsByNameOrPhoneOrEmailAsync(int SupplierId);
    }
}
