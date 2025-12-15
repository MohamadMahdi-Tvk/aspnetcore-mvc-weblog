using System;
using System.Collections.Generic;
using System.Text;

namespace WeblogSample.Service.DTOs.Statistics;

public class StatisticsDto
{
    public long UserCount  { get; set; }
    public long ArticleCount { get; set; }
    public long CommentCount { get; set; }
}
