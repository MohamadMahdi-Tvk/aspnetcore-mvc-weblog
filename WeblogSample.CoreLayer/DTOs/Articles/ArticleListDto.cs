using System;
using System.Collections.Generic;
using System.Text;

namespace WeblogSample.Service.DTOs.Articles;

public class ArticleListDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public int VisitCount { get; set; }
    public string ImagePath { get; set; }
    public string CategoryName { get; set; }
    public string PersonUserName { get; set; }
    public DateTime InsertDate { get; set; }
    public bool IsActive { get; set; }
}
