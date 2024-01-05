namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class TagQuery : IRequest<TagViewModel>
{
    public string NodeId { get; set; }
    public string DeviceId { get; set; }
    public string TagId { get; set; }

    public TagQuery(string nodeId, string deviceId, string tagId)
    {
        NodeId = nodeId;
        DeviceId = deviceId;
        TagId = tagId;
    }
}
