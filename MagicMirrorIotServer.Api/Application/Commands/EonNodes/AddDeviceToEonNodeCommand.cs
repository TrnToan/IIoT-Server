namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

public class AddDeviceToEonNodeCommand: IRequest<bool>
{
    public string EonNodeId { get; set; }
    public string DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string ProtocolId { get; set; }
    public string DeviceProtocol { get; set; }

    public AddDeviceToEonNodeCommand(string eonNodeId, string deviceId, string deviceName, string protocolId, string deviceProtocol)
    {
        EonNodeId = eonNodeId;
        DeviceId = deviceId;
        DeviceName = deviceName;
        ProtocolId = protocolId;
        DeviceProtocol = deviceProtocol;
    }
}
