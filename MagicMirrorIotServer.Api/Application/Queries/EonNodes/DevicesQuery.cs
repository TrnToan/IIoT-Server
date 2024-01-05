namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class DevicesQuery : IRequest<IEnumerable<DeviceViewModel>>
{
    public string NodeId { get; private set; }

    public DevicesQuery(string nodeId)
    {
        NodeId = nodeId;
    }
}
