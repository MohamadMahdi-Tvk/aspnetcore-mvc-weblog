using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Contexts;
using WeblogSample.Data.Entities;
using WeblogSample.Service.Common;
using WeblogSample.Service.DTOs.Articles;
using WeblogSample.Service.DTOs.Comments;
using WeblogSample.Service.Interfaces;
using WeblogSample.Service.Mappers;
using WeblogSample.Service.Mappers.Articles;

namespace WeblogSample.Service.Services;

public class ArticleService : IArticleService
{
    private readonly DataBaseContext _db;
    private readonly ArticleMapper _mapper;

    public ArticleService(DataBaseContext db, ArticleMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<long> CreateAsync(ArticleCreateDto dto)
    {
        var entity = _mapper.ToEntity(dto);
        _db.Articles.Add(entity);
        await _db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task ToggleArticleAsync(long id)
    {
        var article = await _db.Articles.FindAsync(id)
            ?? throw new Exception("Article not found");

        article.IsActive = !article.IsActive;

        await _db.SaveChangesAsync();

    }

    public async Task<List<ArticleListDto>> GetAllAsync()
    {
        var list = await _db.Articles
            .AsNoTracking()
            .Include(a => a.Category)
            .Include(a => a.Person)
            .ToListAsync();

        return list.Select(a => _mapper.ToDto(a)).ToList();
    }

    public async Task<List<ArticleListDto>> GetAllPopularAsync()
    {
        return await _db.Articles
            .OrderByDescending(a => a.VisitCount)
            .Take(4)
            .Where(a => a.IsActive)
            .Select(a => _mapper.ToDto(a))
            .ToListAsync();
    }

    public async Task<ArticleDetailDto> GetByIdAsync(long id)
    {
        await _db.Articles
        .Where(a => a.Id == id)
        .ExecuteUpdateAsync(a =>
        a.SetProperty(x => x.VisitCount, x => x.VisitCount + 1));

        var article = await _db.Articles
        .Include(a => a.Person)
        .Include(a => a.Category)
        .Include(a => a.Comments)
            .ThenInclude(c => c.Replies)
        .Where(a => a.Id == id)
        .Select(a => new ArticleDetailDto
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            ImagePath = a.ImagePath,
            InsertDate = a.InsertDate,
            ShortDescription = a.ShortDescription,
            CategoryId = a.CategoryId,

            AuthorName = a.Person != null ? a.Person.UserName : "ناشناس",
            CategoryName = a.Category != null ? a.Category.Name : "-",

            Comments = a.Comments
                .Where(c => c.ParentId == null && c.IsApproved)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    PersonUserName = c.Person != null ? c.Person.UserName : "کاربر مهمان",
                    Text = c.Text,
                    InsertDate = c.InsertDate,

                    Replies = c.Replies
                        .Where(r => r.IsApproved)
                        .Select(r => new CommentDto
                        {
                            Id = r.Id,
                            PersonUserName = r.Person != null ? r.Person.UserName : "نویسنده",
                            Text = r.Text,
                            InsertDate = r.InsertDate
                        }).ToList()
                }).ToList()
        })
        .FirstOrDefaultAsync();

        return article;
    }

    public async Task<PagedResult<ArticleListDto>> GetLatestArticlesAsync(int pageNumber, int pageSize)
    {
        var query = _db.Articles
        .AsNoTracking()
        .Include(a => a.Category)
        .Include(a => a.Person)
        .Where(a => a.IsActive)
        .OrderByDescending(a => a.InsertDate);

        var totalItems = await query.CountAsync();

        var list = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ArticleListDto>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems,
            Items = list.Select(a => _mapper.ToDto(a)).ToList()
        };
    }

    public async Task<bool> UpdateAsync(ArticleUpdateDto dto)
    {
        var entity = await _db.Articles.FindAsync(dto.Id);
        if (entity == null) return false;

        _mapper.UpdateEntity(dto, entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<PagedResult<ArticleListDto>> GetArticlesByCategoryId(int pageNumber, int pageSize, short categoryId)
    {
        var query = _db.Articles
        .AsNoTracking()
        .Include(a => a.Category)
        .Include(a => a.Person)
        .OrderByDescending(a => a.InsertDate)
        .Where(a => a.CategoryId == categoryId);

        var totalItems = await query.CountAsync();

        var list = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ArticleListDto>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems,
            Items = list.Select(a => _mapper.ToDto(a)).ToList()
        };
    }
}
