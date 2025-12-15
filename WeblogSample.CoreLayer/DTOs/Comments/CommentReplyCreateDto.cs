using System;
using System.Collections.Generic;
using System.Text;

namespace WeblogSample.Service.DTOs.Comments;

public class CommentReplyCreateDto
{
    public long ArticleId { get; set; }
    public long ParentId { get; set; }
    public long PersonId { get; set; }
    public string Text { get; set; }
}
