using BussinessObject.Entities;
using DataAccess.Repositories;
using eStoreAPI.DTO.Category;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepo = new CateogoryRepository();

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var categories = await _categoryRepo.GetCategoriesAsync();
        var categoriesDTOs = categories.Adapt<List<CategoryDTO>>();
        return Ok(categoriesDTOs);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepo.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }

        var categoryDTO = category.Adapt<CategoryDTO>();

        return Ok(categoryDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDTO categoryDTO)
    {
        if (categoryDTO == null)
        {
            return BadRequest("Category data is null.");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        var category = categoryDTO.Adapt<Category>();
        await _categoryRepo.AddCategoryAsync(category);

        return Created(nameof(GetCategoryByIdAsync), new { id = category.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] PutCategoryDTO categoryDTO)
    {
        if (categoryDTO == null)
        {
            return BadRequest("Category data is null.");
        }
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }
        var existingCategory = await _categoryRepo.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }

        categoryDTO.Adapt(existingCategory);

        await _categoryRepo.UpdateCategoryAsync(existingCategory);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        var existingCategory = await _categoryRepo.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }
        await _categoryRepo.DeleteCategoryAsync(id);
        return NoContent();
    }
}
