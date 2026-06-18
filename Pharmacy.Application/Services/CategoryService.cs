using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Domain.Models.PharmacyModels;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Application.Services
{
    public class CategoryService(IUnitOfWork unitOfWork) : Icategoryservice
    {
        public async Task<ApiResponse<CategoryDto>> AddCategoryAsync(CategoryDto categoryDto,CancellationToken cancellationToken)
        {
            if (categoryDto == null) return ApiResponse<CategoryDto>.ErrorResponse("Category data is null.");
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description
            };
            var result = await unitOfWork.categoryReposatory.AddAsync(category,cancellationToken);
            if (result == null) return ApiResponse<CategoryDto>.ErrorResponse("Failed to add category.");
            var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (ResultOfSaving <= 0) return ApiResponse<CategoryDto>.ErrorResponse("Failed to save category.");
            categoryDto.Id = result.Id;
            return ApiResponse<CategoryDto>.Success(categoryDto);
        }
        public async Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var AllCategories = await unitOfWork.categoryReposatory.GetAllAsync(cancellationToken);
            if (AllCategories == null) return ApiResponse<IEnumerable<CategoryDto>>.ErrorResponse("No Categories found.");
            var CategoryDtos = new List<CategoryDto>();
            foreach (var Category in AllCategories)
            {
                var CategoryDto = new CategoryDto
                {
                    Id = Category.Id,
                    CategoryName = Category.CategoryName,
                    Description = Category.Description
                };
                CategoryDtos.Add(CategoryDto);
            }
            return ApiResponse<IEnumerable<CategoryDto>>.Success(CategoryDtos);
        }

        public async Task<ApiResponse<CategoryDto>> GetCategoryAsync(string input, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var Category = await unitOfWork.categoryReposatory.GetCategoryAsync(input, cancellationToken);
            if (Category == null) return ApiResponse<CategoryDto>.ErrorResponse("Category not found.");
            var CategoryDto = new CategoryDto
            {
                Id = Category.Id,
                CategoryName = Category.CategoryName,
                Description = Category.Description
            };
            return ApiResponse<CategoryDto>.Success(CategoryDto);
        }
        public async Task<ApiResponse<CategoryDto>> GetCategoryAsync(int Id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var Category = await unitOfWork.categoryReposatory.GetCategoryAsync(Id, cancellationToken);
            if (Category == null) return ApiResponse<CategoryDto>.ErrorResponse("Category not found.");
            var categoryDto = new CategoryDto
            {
                Id = Category.Id,
                CategoryName = Category.CategoryName,
                Description = Category.Description
            };
            return ApiResponse<CategoryDto>.Success(categoryDto);
        }

        public async Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var CheckCategory = await unitOfWork.categoryReposatory.GetByIdAsync(categoryDto.Id, cancellationToken);
            if (CheckCategory == null) return ApiResponse<CategoryDto>.ErrorResponse("Category not found.");
            CheckCategory.CategoryName = categoryDto.CategoryName;
            CheckCategory.Description = categoryDto.Description;
            unitOfWork.categoryReposatory.Update(CheckCategory, cancellationToken);
            var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (ResultOfSaving <= 0) return ApiResponse<CategoryDto>.ErrorResponse("Failed to update supplier.");
            return ApiResponse<CategoryDto>.Success(categoryDto);
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int Id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var CheckCategory = await unitOfWork.categoryReposatory.GetByIdAsync(Id, cancellationToken);
            if (CheckCategory == null) return ApiResponse<bool>.ErrorResponse("Category not found.");
            unitOfWork.categoryReposatory.Delete(CheckCategory, cancellationToken);
            var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (ResultOfSaving <= 0) return ApiResponse<bool>.ErrorResponse("Failed to delete Category.");
            return ApiResponse<bool>.Success(true);
        }
    }
}

