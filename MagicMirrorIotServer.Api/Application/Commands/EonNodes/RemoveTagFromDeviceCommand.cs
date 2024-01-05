namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class RemoveTagFromDeviceCommand : IRequest<bool>
{
    public string NodeId { get; set; }
    public string DeviceId { get; set; }
    public string TagId { get; set; }

    public RemoveTagFromDeviceCommand(string nodeId, string deviceId, string tagId)
    {
        NodeId = nodeId;
        DeviceId = deviceId;
        TagId = tagId;
    }
}
