using System.ComponentModel.DataAnnotations;

namespace WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Comments;

public class CreateCommentViewModel
{
    [Required(ErrorMessage = "متن کامنت نباید خالی باشد")]
    [StringLength(500, ErrorMessage = "متن کامنت نباید بیش از 500 کارکتر باشد")]
    public string Text { get; set; }
    public long? ParentId { get; set; }
    public long? PersonId { get; set; }
    public long ArticleId { get; set; }
    public bool IsApproved { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
