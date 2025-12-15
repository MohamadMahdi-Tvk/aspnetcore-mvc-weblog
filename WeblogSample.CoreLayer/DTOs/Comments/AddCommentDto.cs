using System;
using System.Collections.Generic;
using System.Text;

namespace WeblogSample.Service.DTOs.Comments;

public class AddCommentDto
{
    public long ArticleId { get; set; }
    public string Text { get; set; }
    public long PersonId { get; set; }
}
