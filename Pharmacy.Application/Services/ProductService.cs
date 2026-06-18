using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Application.Services.Integration_Service;
using Pharmacy.Application.Services.Integration_Service;
using Pharmacy.Domain.Models.PharmacyModels;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Application.Services
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService fileService;

        public ProductService(IUnitOfWork unitOfWork,IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            this.fileService = fileService;
        }
        public async Task<ApiResponse<productDto>> AddProductAsync(productDto productDto,CancellationToken cancellationToken)
        {
            if (productDto == null) return ApiResponse<productDto>.ErrorResponse("Product data is null.");
            var Product = new Product
            {
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                ExpirationDate = productDto.ExpirationDate,
                Barcode = productDto.Barcode,
                PrescriptionRequired = productDto.PrescriptionRequired,
                CategoryId = productDto.CategoryId,
                SupplierId = productDto.SupplierId
            };
            var ImageUrl = await fileService.SaveFileAsync(productDto.formFile);
            Product.ImageUrl = ImageUrl;
            var result = await unitOfWork.productReposatory.AddAsync(Product,cancellationToken);
            if (result == null) return ApiResponse<productDto>.ErrorResponse("Failed to add product.");
            var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (ResultOfSaving <= 0) return ApiResponse<productDto>.ErrorResponse("Failed to save product.");
            productDto.Id = result.Id;
            return ApiResponse<productDto>.Success(productDto);
        }       
        public async Task<ApiResponse<IEnumerable<productDto>>> AddRangeProductAsync(IEnumerable<productDto> productDto,CancellationToken cancellationToken)
        {
            if (productDto == null) return ApiResponse<IEnumerable<productDto>>.ErrorResponse("Product data is null.");
            List<productDto> products = new List<productDto>();
            foreach (var item in productDto)
            {
                var Product = new Product
                {
                    ProductName = item.ProductName,
                    Description = item.Description,
                    Price = item.Price,
                    StockQuantity = item.StockQuantity,
                    ExpirationDate = item.ExpirationDate,
                    Barcode = item.Barcode,
                    PrescriptionRequired = item.PrescriptionRequired,
                    CategoryId = item.CategoryId,
                    SupplierId = item.SupplierId
                };
                var ImageUrl = await fileService.SaveFileAsync(item.formFile);
                Product.ImageUrl = ImageUrl;
                var result = await unitOfWork.productReposatory.AddAsync(Product,cancellationToken);
                if (result == null) return ApiResponse<IEnumerable<productDto>>.ErrorResponse("Failed to add product.");
                var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
                if (ResultOfSaving <= 0) return ApiResponse<IEnumerable<productDto>>.ErrorResponse("Failed to save product.");
                item.Id = result.Id;
                products.Add(item);
            }
            return ApiResponse<IEnumerable<productDto>>.Success(products);

        }

        public async Task<ApiResponse<int>> CountAsync(CancellationToken cancellationToken)
        {
           var Count = await unitOfWork.productReposatory.CountAsync(cancellationToken);
            if(Count ==0) return ApiResponse<int>.ErrorResponse("No products found.",404);
            return ApiResponse<int>.Success(Count);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id,CancellationToken cancellationToken)
        {
            var product =await unitOfWork.productReposatory.GetByIdAsync(id,cancellationToken);
            var ProductUrl = product.ImageUrl;
            var DeletedFile =fileService.DeleteFile(ProductUrl);
            if (product != null)
            {
                unitOfWork.productReposatory.Delete(product,cancellationToken);
            }
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);
            if(result <= 0) return ApiResponse<bool>.ErrorResponse("Failed to delete product.");
            return ApiResponse<bool>.Success(true);
        }

        
        public Task<ApiResponse<IEnumerable<productDto>>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<productDto>> GetProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<productDto>>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<productDto>>> GetProductsBySupplierAsync(int SupplierId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<productDto>>> SearchProductsByNameOrPhoneOrEmailAsync(int SupplierId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(productDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
