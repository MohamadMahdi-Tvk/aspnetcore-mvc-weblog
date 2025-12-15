using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "وارد کردن نام کاربری اجباری است")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "تعداد کارکتر های مجاز بین 2 تا 20 کارکتر است")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "وارد کردن رمز عبور اجباری است")]
    [MinLength(6, ErrorMessage = "حداقل تعداد کارکتر باید 6 باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = "وارد کردن تکرار رمز عبور اجباری است")]
    [Compare(nameof(Password),ErrorMessage = "رمز عبور با تکرار آن مطابقت ندارد")]
    public string ConfirmPassword { get; set; }
}
