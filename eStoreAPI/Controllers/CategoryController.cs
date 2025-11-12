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
    private readonly ICategoryRepository categoryRepo = new CateogoryRepository();

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoryRepo.GetCategoriesAsync();
        var categoriesDTOs = categories.Adapt<List<CategoryDTO>>();
        return Ok(categoriesDTOs);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await categoryRepo.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }

        var categoryDTO = category.Adapt<CategoryDTO>();

        return Ok(categoryDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDTO)
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
        await categoryRepo.AddCategoryAsync(category);

        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] PutCategoryDTO categoryDTO)
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
        var existingCategory = await categoryRepo.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }

        categoryDTO.Adapt(existingCategory);

        await categoryRepo.UpdateCategoryAsync(existingCategory);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var existingCategory = await categoryRepo.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }
        await categoryRepo.DeleteCategoryAsync(id);
        return NoContent();
    }
}
