using BussinessObject.Entities;
using DataAccess.Repositories;
using eStoreAPI.DTO.Product;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepo = new ProductRepository();

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await productRepo.GetProductsAsync();
        var productDTOs = products.Adapt<List<ProductDTO>>();

        return Ok(productDTOs);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync(int id)
    {
        var product = await productRepo.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        var productDTO = product.Adapt<ProductDTO>();
        return Ok(productDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDTO createProductDTO)
    {
        if (createProductDTO == null)
        {
            return BadRequest("Product data is required.");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        var product = createProductDTO.Adapt<Product>();
        await productRepo.AddProductAsync(product);

        return Created(nameof(GetProductByIdAsync), new { id = product.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] PutProductDTO putProductDTO)
    {
        if (putProductDTO == null)
        {
            return BadRequest("Product data is required.");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        var existingProduct = await productRepo.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        putProductDTO.Adapt(existingProduct);
        await productRepo.UpdateProductAsync(existingProduct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        var existingProduct = await productRepo.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        await productRepo.DeleteProductAsync(id);
        return NoContent();
    }
}
