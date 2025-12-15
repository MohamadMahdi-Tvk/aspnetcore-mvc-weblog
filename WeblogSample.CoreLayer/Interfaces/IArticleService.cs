using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Service.Common;
using WeblogSample.Service.DTOs.Articles;

namespace WeblogSample.Service.Interfaces;

public interface IArticleService
{
    Task<List<ArticleListDto>> GetAllAsync();
    Task<List<ArticleListDto>> GetAllPopularAsync();
    Task<PagedResult<ArticleListDto>> GetLatestArticlesAsync(int pageNumber, int pageSize);
    Task<ArticleDetailDto> GetByIdAsync(long id);
    Task<long> CreateAsync(ArticleCreateDto dto);
    Task<bool> UpdateAsync(ArticleUpdateDto dto);
    Task ToggleArticleAsync(long id);
    Task<PagedResult<ArticleListDto>> GetArticlesByCategoryId(int pageNumber, int pageSize, short categoryId);
}
