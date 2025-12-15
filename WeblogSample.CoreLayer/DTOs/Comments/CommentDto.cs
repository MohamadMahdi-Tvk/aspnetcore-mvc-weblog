using System;
using System.Collections.Generic;
using System.Text;

namespace WeblogSample.Service.DTOs.Comments;

public class CommentDto
{
    public long Id { get; set; }
    public string Text { get; set; }
    public string PersonUserName { get; set; }
    public DateTime InsertDate { get; set; }
    public long? ParentId { get; set; }

    public long? PersonId { get; set; }
    public long ArticleId { get; set; }
    public bool IsApproved { get; set; }
    public List<CommentDto> Replies { get; set; }
}
