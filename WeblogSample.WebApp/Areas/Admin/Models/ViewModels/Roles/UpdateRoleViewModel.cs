using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Roles;

public class UpdateRoleViewModel
{
    public short Id { get; set; }

    [Required(ErrorMessage = "نقش کاربری نباید خالی بماند")]
    [StringLength(20, ErrorMessage = "تعداد کارکتر های نقش کاربری نباید بیشتر از 20 کارکتر باشد")]
    public string Name { get; set; }
}