using System.ComponentModel.DataAnnotations;

namespace MT.Shared.DataTransferModels.Category;

public class CategoryModel
{
    public int Id { get; set; }
    public string HashId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title cannot be more than 100 characters")]
    public string Title { get; set; }
}
