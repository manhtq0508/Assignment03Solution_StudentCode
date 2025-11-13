using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.DTO.Category;

public class PutCategoryDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "CategoryName is required.")]
    public string Name { get; set; } = string.Empty;
}
