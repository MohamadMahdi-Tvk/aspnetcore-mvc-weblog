using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Articles;

public class CreateArticleViewModel
{

    [Required(ErrorMessage = "وارد کردن موضوع مقاله الزامی است")]
    [StringLength(30, ErrorMessage = "موضوع مقاله نمی‌تواند بیشتر از 30 کاراکتر باشد")]
    public string Title { get; set; }



    [Required(ErrorMessage = "توضیح کوتاه مقاله الزامی است")]
    [StringLength(500, ErrorMessage = "توضیح کوتاه نمی‌تواند بیشتر از 500 کاراکتر باشد")]
    public string ShortDescription { get; set; }



    [Required(ErrorMessage = "متن مقاله نمی‌تواند خالی باشد")]
    public string Description { get; set; }


    [BindNever]
    public string? ImagePath { get; set; }


    [DisplayName("تعداد بازدید")]
    public int VisitCount { get; set; } = 0;



    [Required(ErrorMessage = "عکس مقاله نباید خالی باشد")]
    [DataType(DataType.Upload)]
    public IFormFile ImageFile { get; set; }



    [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است")]
    public short? CategoryId { get; set; }

}
