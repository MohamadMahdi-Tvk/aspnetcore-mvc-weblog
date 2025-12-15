using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Entities;
using WeblogSample.Service.DTOs.Articles;

namespace WeblogSample.Service.Mappers.Articles;

public class ArticleMapper :
    IMapper<Article, ArticleListDto>,
    IMapper<Article, ArticleDetailDto>,
    IUpdateMapper<ArticleUpdateDto, Article>,
    ICreateMapper<ArticleCreateDto, Article>
{
    public ArticleListDto ToDto(Article entity)
    {
        return new ArticleListDto()
        {
            Id = entity.Id,
            Title = entity.Title,
            ShortDescription = entity.ShortDescription,
            CategoryName = entity.Category?.Name,
            PersonUserName = entity.Person?.UserName,
            VisitCount = entity.VisitCount,
            InsertDate = entity.InsertDate,
            ImagePath = entity.ImagePath,
            IsActive = entity.IsActive
        };
    }

    public Article ToEntity(ArticleCreateDto dto)
    {
        return new Article
        {
            Title = dto.Title,
            ShortDescription = dto.ShortDescription,
            Description = dto.Description,
            ImagePath = dto.ImagePath,
            CategoryId = dto.CategoryId,
            PersonId = dto.PersonId,
            InsertDate = DateTime.Now,
            IsActive = true,
            VisitCount = dto.VisitCount
        };
    }

    public void UpdateEntity(ArticleUpdateDto dto, Article entity)
    {
        entity.Title = dto.Title;
        entity.ShortDescription = dto.ShortDescription;
        entity.Description = dto.Description;
        entity.ImagePath = dto.ImagePath;
        entity.CategoryId = dto.CategoryId;
        entity.UpdateDate = DateTime.Now;
    }

    public ArticleDetailDto ToDetailDto(Article article)
    {
        return new ArticleDetailDto
        {
            Id = article.Id,
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            ImagePath = article.ImagePath,
            Description = article.Description,
            AuthorName = article.Person.UserName,
            CategoryName = article.Category.Name,
            VisitCount = article.VisitCount,
            InsertDate = article.InsertDate
        };
    }

    ArticleDetailDto IMapper<Article, ArticleDetailDto>.ToDto(Article entity)
    {
        return new ArticleDetailDto
        {
            Id = entity.Id,
            Title = entity.Title,
            ShortDescription = entity.ShortDescription,
            Description = entity.Description,
            ImagePath = entity.ImagePath,
            AuthorName = entity.Person.UserName,
            CategoryName = entity.Category.Name,
            VisitCount = entity.VisitCount,
            InsertDate = entity.InsertDate
        };
    }
}
