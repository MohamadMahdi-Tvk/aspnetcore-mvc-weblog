using Microsoft.AspNetCore.Mvc;
using WeblogSample.Service.Interfaces;

namespace WeblogSample.WebApp.Components;

public class StatisticsViewComponent : ViewComponent
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsViewComponent(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var statistics = await _statisticsService.GetStatistics();

        return View(statistics);
    }
}
