using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Categories;

public class CreateCategoryViewModel
{
    [Required(ErrorMessage = "نام دسته بندی نباید خالی باشد")]
    [StringLength(20,ErrorMessage = "تعداد کارکتر های مجاز 20 کارکتر است")]
    public string Name { get; set; }
}
