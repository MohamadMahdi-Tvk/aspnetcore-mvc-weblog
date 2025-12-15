using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Service.DTOs.Comments;

namespace WeblogSample.Service.Interfaces;

public interface ICommentService
{
    Task<List<CommentDto>> GetAllByArticleIdAsync(long articleId);
    Task<CommentDto> GetByIdAsync(long id);
    Task<long> CreateAsync(CommentCreateDto dto);
    Task ReplayComment(CommentReplayDto dto);
    Task CreateReplyAsync(CommentReplyCreateDto dto);
    Task AddAsync(AddCommentDto dto);
    Task<bool> DeleteAsync(long id);
}
