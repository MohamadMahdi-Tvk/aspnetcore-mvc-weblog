using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Data.Contexts;
using WeblogSample.Service.DTOs.Statistics;
using WeblogSample.Service.Interfaces;

namespace WeblogSample.Service.Services;

public class StatisticsService : IStatisticsService
{
    private readonly DataBaseContext _context;

    public StatisticsService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<StatisticsDto> GetStatistics()
    {
        var userCount = _context.People.Count();
        var articleCount = _context.Articles.Count();
        var commentCount = _context.Comments.Count();

        var Statistics = new StatisticsDto
        {
            UserCount = userCount,
            ArticleCount = articleCount,
            CommentCount = commentCount
        };

        return Statistics;
    }
}
