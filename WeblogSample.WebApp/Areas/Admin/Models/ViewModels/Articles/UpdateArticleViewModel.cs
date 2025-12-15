using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Articles;

public class UpdateArticleViewModel
{
    public long Id { get; set; }


    [Required(ErrorMessage = "وارد کردن موضوع مقاله الزامی است")]
    [StringLength(30, ErrorMessage = "موضوع مقاله نمی‌تواند بیشتر از 30 کاراکتر باشد")]
    public string Title { get; set; }



    [Required(ErrorMessage = "توضیح کوتاه مقاله الزامی است")]
    [StringLength(500, ErrorMessage = "توضیح کوتاه نمی‌تواند بیشتر از 500 کاراکتر باشد")]
    public string ShortDescription { get; set; }


    [Required(ErrorMessage = "متن مقاله نمی‌تواند خالی باشد")]
    public string Description { get; set; }



    [DataType(DataType.Upload)]
    public IFormFile? ImageFile { get; set; }


    [BindNever]
    public string? ImagePath { get; set; }


    public long? PersonId { get; set; }


    [DisplayName("دسته‌بندی")]
    [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است")]
    public short? CategoryId { get; set; }
}
