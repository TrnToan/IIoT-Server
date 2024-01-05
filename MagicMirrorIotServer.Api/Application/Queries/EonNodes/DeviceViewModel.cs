namespace MagicMirrorIotServer.Api.Application.Queries.EonNodes;

public class DeviceViewModel
{
    public string DeviceId { get; private set; }
    public string DeviceName { get; private set; }
    public string ProtocolId { get; private set; }
    public string DeviceProtocol { get; private set; }

    public DeviceViewModel(string deviceId, string deviceName, string protocolId, string deviceProtocol)
    {
        DeviceId = deviceId;
        DeviceName = deviceName;
        ProtocolId = protocolId;
        DeviceProtocol = deviceProtocol;
    }

    public DeviceViewModel()
    {

    }
}
