using WeblogSample.Data.Entities;
using WeblogSample.Service.DTOs.Comments;

namespace WeblogSample.Service.Mappers.Comments;

public class CommentMapper : IMapper<Comment, CommentDto>, ICreateMapper<CommentCreateDto, Comment>
{
    public CommentDto ToDto(Comment entity)
    {
        return new CommentDto
        {
            Id = entity.Id,
            Text = entity.Text,
            PersonUserName = entity.Person?.UserName,
            ArticleId = entity.ArticleId,
            ParentId = entity.ParentId,
            InsertDate = entity.InsertDate,
            Replies = entity.Replies?.Select(r => ToDto(r)).ToList(),
            IsApproved = entity.IsApproved,
        };
    }


    public Comment ToEntity(CommentCreateDto dto)
    {
        return new Comment
        {
            Text = dto.Text,
            PersonId = dto.PersonId,
            ArticleId = dto.ArticleId,
            InsertDate = DateTime.Now,
            IsActive = true
        };
    }
}
