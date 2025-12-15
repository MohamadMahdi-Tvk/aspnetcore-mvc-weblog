using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.People;

public class CreatePersonViewModel
{
    [Required(ErrorMessage = "وارد کردن یوزرنیم الزامی است")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "تعداد کارکتر های مجاز بین 2 تا 20 کارکتر است")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "وارد کردن پسورد الزامی است")]
    [MinLength(6, ErrorMessage = "حداقل تعداد کارکتر باید 6 باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    [Required(ErrorMessage = "انتخاب نقش کاربری الزامی است")]
    public short? RoleId { get; set; }

}
