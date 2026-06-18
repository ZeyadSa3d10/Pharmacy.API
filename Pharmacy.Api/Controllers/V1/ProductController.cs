using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Interfaces.Reposatory;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;

namespace Pharmacy.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;

        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] productDto product,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productServices.AddProductAsync(product,cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("AddRangeOfProducts")]
        public async Task<IActionResult> AddRangeOfProducts(IEnumerable<productDto> productDto,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productServices.AddRangeProductAsync(productDto,cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("CountOfProducts")]
        public async Task<IActionResult> GetCountOfProducts(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productServices.CountAsync(cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productServices.DeleteAsync(id,cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}