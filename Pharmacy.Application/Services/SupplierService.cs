using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Common;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Domain.Models.PharmacyModels;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Application.Services
{
    public class SupplierService(IUnitOfWork unitOfWork) : ISupplierService
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ApiResponse<SupplierDto>> AddSupplierAsync(SupplierDto supplierDto,CancellationToken cancellationToken)
        {
            if (supplierDto == null) return ApiResponse<SupplierDto>.ErrorResponse("Category data is null.");
            var supplier = new Supplier
            {
                SupplierName = supplierDto.SupplierName,
                Address = supplierDto.Address,
                Email = supplierDto.Email,
                PhoneNumber = supplierDto.PhoneNumber
            };
            var result = await unitOfWork.supplierReposatory.AddAsync(supplier,cancellationToken);
            if (result == null) return ApiResponse<SupplierDto>.ErrorResponse("Failed to add category.");
            var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (ResultOfSaving <= 0) return ApiResponse<SupplierDto>.ErrorResponse("Failed to save category.");
            supplierDto.Id = result.Id;
            return ApiResponse<SupplierDto>.Success(supplierDto);
        }

        public async Task<ApiResponse<IEnumerable<SupplierDto>>> GetAllSuppliersAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var AllSuppliers =await unitOfWork.supplierReposatory.GetAllAsync(cancellationToken);
            if(AllSuppliers  == null) return ApiResponse<IEnumerable<SupplierDto>>.ErrorResponse("No suppliers found.");
            var supplierDtos = new List<SupplierDto>();
            foreach (var Supplier in AllSuppliers)
            {
                var supplierDto = new SupplierDto
                {
                    Id = Supplier.Id,
                    SupplierName = Supplier.SupplierName,
                    Address = Supplier.Address,
                    Email = Supplier.Email,
                    PhoneNumber = Supplier.PhoneNumber
                };
                supplierDtos.Add(supplierDto);
            }
            return ApiResponse<IEnumerable<SupplierDto>>.Success(supplierDtos);
        }

        public async Task<ApiResponse<SupplierDto>> GetSupplierAsync(string input, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var supplier =await unitOfWork.supplierReposatory.GetSupplierAsync(input, cancellationToken);
            if (supplier == null) return ApiResponse<SupplierDto>.ErrorResponse("Supplier not found.");
            var supplierDto = new SupplierDto
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                NationalId = supplier.NationalId,
                Address = supplier.Address,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber
            };
            return ApiResponse<SupplierDto>.Success(supplierDto);
        }
        public async Task<ApiResponse<SupplierDto>> GetSupplierAsync(int Id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var supplier =await unitOfWork.supplierReposatory.GetSupplierAsync(Id, cancellationToken);
            if (supplier == null) return ApiResponse<SupplierDto>.ErrorResponse("Supplier not found.");
            var supplierDto = new SupplierDto
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                NationalId = supplier.NationalId,
                Address = supplier.Address,
                Email = supplier.Email,
                PhoneNumber = supplier.PhoneNumber
            };
            return ApiResponse<SupplierDto>.Success(supplierDto);
        }

        public async Task<ApiResponse<SupplierDto>> UpdateSupplierAsync(SupplierDto supplierDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var CheckSupplier =await unitOfWork.supplierReposatory.GetByIdAsync(supplierDto.Id, cancellationToken);
            if (CheckSupplier == null) return ApiResponse<SupplierDto>.ErrorResponse("Supplier not found.");
            CheckSupplier.SupplierName = supplierDto.SupplierName;
            CheckSupplier.Address = supplierDto.Address;
            CheckSupplier.Email = supplierDto.Email;
            CheckSupplier.PhoneNumber = supplierDto.PhoneNumber;
            CheckSupplier.NationalId = supplierDto.NationalId;
            unitOfWork.supplierReposatory.Update(CheckSupplier,cancellationToken);
            var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (ResultOfSaving <= 0) return ApiResponse<SupplierDto>.ErrorResponse("Failed to update supplier.");
            return ApiResponse<SupplierDto>.Success(supplierDto);
        }

        public async Task<ApiResponse<bool>> DeleteSupplier(string NationalId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var CheckSupplier =await unitOfWork.supplierReposatory.GetSupplierAsync(NationalId, cancellationToken);
            if (CheckSupplier == null) return ApiResponse<bool>.ErrorResponse("Supplier not found.");
            unitOfWork.supplierReposatory.Delete(CheckSupplier, cancellationToken);
            var ResultOfSaving = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (ResultOfSaving <= 0) return ApiResponse<bool>.ErrorResponse("Failed to delete supplier.");
            return ApiResponse<bool>.Success(true);
        }
    }
}
