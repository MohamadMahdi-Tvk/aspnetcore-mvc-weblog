using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "وارد کردن نام کاربری اجباری است")]
    [StringLength(20,MinimumLength = 2,ErrorMessage = "تعداد کارکتر های مجاز بین 2 تا 20 کارکتر است")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "وارد کردن رمز عبور اجباری است")]
    public string Password { get; set; }
}
