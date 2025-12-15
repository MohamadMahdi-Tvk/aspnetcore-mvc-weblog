using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.People;

public class UpdatePersonViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "نام کاربری الزامی است")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "تعداد کارکتر های مجاز بین 2 تا 20 کارکتر است")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "انتخاب نقش کاربری الزامی است")]
    public short? RoleId { get; set; }

}
