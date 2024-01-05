namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class RemoveDeviceFromNodeCommand : IRequest<bool>
{
    public string NodeId { get; set; }
    public string DeviceId { get; set; }

    public RemoveDeviceFromNodeCommand(string nodeId, string deviceId)
    {
        NodeId = nodeId;
        DeviceId = deviceId;
    }
}
