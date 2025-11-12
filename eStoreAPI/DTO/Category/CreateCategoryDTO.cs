using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.DTO.Category;

public class CreateCategoryDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Category name is required.")]
    public string Name { get; set; } = string.Empty;
}
