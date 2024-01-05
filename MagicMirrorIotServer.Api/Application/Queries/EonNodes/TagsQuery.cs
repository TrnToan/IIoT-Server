namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class TagsQuery : IRequest<IEnumerable<TagViewModel>>
{
    public string NodeId { get; set; }
    public string DeviceId { get; set; }

    public TagsQuery(string nodeId, string deviceId)
    {
        NodeId = nodeId;
        DeviceId = deviceId;
    }
}
