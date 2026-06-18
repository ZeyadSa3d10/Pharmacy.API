using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SupplierController(ISupplierService supplierService) : ControllerBase
    {
        private readonly ISupplierService supplierService = supplierService;

        [HttpPost("AddSupplier")]
        public async Task<IActionResult> AddSupplier([FromForm] SupplierDto supplierDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await supplierService.AddSupplierAsync(supplierDto, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("GetAllSuppliers")]
        public async Task<IActionResult> GetAllSuppliers(CancellationToken cancellationToken)
        {
            var result = await supplierService.GetAllSuppliersAsync(cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("GetSupplierByID")]
        public async Task<IActionResult> GetSupplierByID(int Id, CancellationToken cancellationToken)
        {
            var result = await supplierService.GetSupplierAsync(Id, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("GetSupplier")]
        public async Task<IActionResult> GetSupplier(string Input, CancellationToken cancellationToken)
        {
            var result = await supplierService.GetSupplierAsync(Input, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier(string NationalId, CancellationToken cancellationToken)
        {
            var result = await supplierService.DeleteSupplier(NationalId, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(SupplierDto supplierDto, CancellationToken cancellationToken)
        {
            var result = await supplierService.UpdateSupplierAsync(supplierDto, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}   
