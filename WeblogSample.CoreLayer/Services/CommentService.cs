using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WeblogSample.Data.Contexts;
using WeblogSample.Data.Entities;
using WeblogSample.Service.DTOs.Comments;
using WeblogSample.Service.Interfaces;
using WeblogSample.Service.Mappers.Comments;

namespace WeblogSample.Service.Services;

public class CommentService : ICommentService
{
    private readonly DataBaseContext _db;
    private readonly CommentMapper _mapper;

    public CommentService(DataBaseContext db, CommentMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<List<CommentDto>> GetAllByArticleIdAsync(long articleId)
    {
        var list = await _db.Comments
            .Include(c => c.Article)
            .Where(c => c.ArticleId == articleId && c.ParentId == null)
            .Include(c => c.Replies)
            .ThenInclude(r => r.Person)
            .Include(c => c.Person)

            .ToListAsync();

        return list.Select(c => _mapper.ToDto(c)).ToList();
    }

    public async Task<CommentDto> GetByIdAsync(long id)
    {
        var entity = await _db.Comments
            .AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Id == id);

        return entity == null ? null : _mapper.ToDto(entity);
    }

    public async Task<long> CreateAsync(CommentCreateDto dto)
    {

        if (dto.ParentId.HasValue)
        {
            var parentComment = await _db.Comments
                .Where(c => c.Id == dto.ParentId.Value)
                .Select(c => new
                {
                    c.Id,
                    c.ArticleId,
                    c.ParentId
                })
                .FirstOrDefaultAsync();

            if (parentComment == null)
                throw new Exception("کامنت والد یافت نشد");

            if (parentComment.ParentId != null)
                throw new Exception("امکان پاسخ به پاسخ وجود ندارد");


            dto.ArticleId = parentComment.ArticleId;
        }
        else
        {

            if (!await _db.Articles.AnyAsync(a => a.Id == dto.ArticleId))
                throw new Exception("مقاله معتبر نیست");
        }

        var entity = _mapper.ToEntity(dto);

        _db.Comments.Add(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _db.Comments.FindAsync(id);
        if (entity == null) return false;

        entity.IsApproved = !entity.IsApproved;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task CreateReplyAsync(CommentReplyCreateDto dto)
    {
        var reply = new Comment
        {
            ArticleId = dto.ArticleId,
            ParentId = dto.ParentId,
            PersonId = dto.PersonId,
            Text = dto.Text,
            IsApproved = true
        };

        _db.Comments.Add(reply);

        await _db.SaveChangesAsync();
    }

    public async Task AddAsync(AddCommentDto dto)
    {

        var comment = new Comment
        {
            ArticleId = dto.ArticleId,
            Text = dto.Text,
            IsApproved = true,
            PersonId = dto.PersonId
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();
    }

    public async Task ReplayComment(CommentReplayDto dto)
    {
        var comment = await _db.Comments.FindAsync(dto.ParentId);

        var replayComment = new Comment
        {
            ParentId = comment.Id,
            ArticleId = comment.ArticleId,
            Text = dto.Text,
            IsApproved = true,
            PersonId = dto.PersonId,
            InsertDate = dto.InsertDate,
            UpdateDate = dto.UpdateDate,

        };

        await _db.Comments.AddAsync(replayComment);

        await _db.SaveChangesAsync();

    }

}
