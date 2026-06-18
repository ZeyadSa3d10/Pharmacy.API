using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Domain.Interfaces.Service
{
    public interface Icategoryservice
    {
        public Task<ApiResponse<CategoryDto>> AddCategoryAsync(CategoryDto categoryDto,CancellationToken cancellationToken);
        public Task<ApiResponse<bool>> DeleteCategoryAsync(int ID, CancellationToken cancellationToken);
        public Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken);
        public Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync(CancellationToken cancellationToken);
        public Task<ApiResponse<CategoryDto>> GetCategoryAsync(string input, CancellationToken cancellationToken);
        public Task<ApiResponse<CategoryDto>> GetCategoryAsync(int Id, CancellationToken cancellationToken);

    }
}
