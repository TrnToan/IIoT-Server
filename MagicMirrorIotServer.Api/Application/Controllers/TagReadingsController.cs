using MagicMirrorIotServer.Api.Application.Queries;
using MagicMirrorIotServer.Api.Application.Queries.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace MagicMirrorIotServer.Api.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagReadingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagReadingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("doubleTagReadings/cycleTime")]
    public async Task<IEnumerable<CycleTimeGraphViewModel>> GetAsync([FromQuery]string eonNodeId, [FromQuery]TimeRangeQuery timeRange)
    {
        CycleTimeGraphQuery query = new CycleTimeGraphQuery(eonNodeId);
        query.StartTime = timeRange.StartTime;
        query.EndTime = timeRange.EndTime;
        return await _mediator.Send(query);
    }
}
