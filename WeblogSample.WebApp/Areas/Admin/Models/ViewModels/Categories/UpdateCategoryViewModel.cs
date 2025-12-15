using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Categories;

public class UpdateCategoryViewModel
{
    public short Id { get; set; }

    [Required(ErrorMessage = "نام دسته بندی نباید خالی باشد")]
    [StringLength(20, ErrorMessage = "تعداد کارکتر های مجاز 20 کارکتر است")]
    public string Name { get; set; }
}