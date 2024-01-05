namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class EonNodesQuery : IRequest<IEnumerable<EonNodeViewModel>>
{
    public string? GetAll { get; private set; } = string.Empty;
}
