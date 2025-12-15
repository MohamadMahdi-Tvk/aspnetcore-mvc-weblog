using System;
using System.Collections.Generic;
using System.Text;
using WeblogSample.Service.DTOs.Statistics;

namespace WeblogSample.Service.Interfaces;

public interface IStatisticsService
{
    Task<StatisticsDto> GetStatistics();
}
