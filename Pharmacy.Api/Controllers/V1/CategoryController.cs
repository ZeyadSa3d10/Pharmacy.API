using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Services;
using Pharmacy.Domain.ApplicationDtos.ProductDtos;
using Pharmacy.Domain.Interfaces.Service;

namespace Pharmacy.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CategoryController(CategoryService categoryService) : ControllerBase
    {
        private readonly CategoryService categoryService = categoryService;

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromForm] CategoryDto category,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await categoryService.AddCategoryAsync(category,cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("GetAllCategorys")]
        public async Task<IActionResult> GetAllSuppliers(CancellationToken cancellationToken)
        {
            var result = await categoryService.GetAllCategoryAsync(cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("GetCategoryByID")]
        public async Task<IActionResult> GetCategoryByID(int Id, CancellationToken cancellationToken)
        {
            var result = await categoryService.GetCategoryAsync(Id, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategoryByID(string Input, CancellationToken cancellationToken)
        {
            var result = await categoryService.GetCategoryAsync(Input, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int Id, CancellationToken cancellationToken)
        {
            var result = await categoryService.DeleteCategoryAsync(Id, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var result = await categoryService.UpdateCategoryAsync(categoryDto, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
