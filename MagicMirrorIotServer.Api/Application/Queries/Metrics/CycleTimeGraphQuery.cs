namespace MagicMirrorIotServer.Api.Application.Queries.Metrics;

public class CycleTimeGraphQuery : TimeRangeQuery, IRequest<IEnumerable<CycleTimeGraphViewModel>>
{
    public string EonNodeId { get; private set; }

    public CycleTimeGraphQuery(string eonNodeId)
    {
        EonNodeId = eonNodeId;
    }
}
