using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Models;

public class AddCommentViewModel
{
    public long ArticleId { get; set; }


    [Required(ErrorMessage = "نظر شما نمی‌تواند خالی باشد")]
    [StringLength(500, ErrorMessage = "تعداد کارکتر های کامنت بیش از حد مجاز است")]
    public string Text { get; set; }

}
