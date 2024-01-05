namespace MagicMirrorIotServer.Api.Application.Commands.EonNodes;

[DataContract]
public class CreateDeviceViewModel
{
    [DataMember]
    public string DeviceId { get; set; }
    [DataMember]
    public string DeviceName { get; set; }
    [DataMember]
    public string ProtocolId { get; set; }
    [DataMember]
    public string DeviceProtocol { get; set; }

    public CreateDeviceViewModel(string deviceId, string deviceName, string protocolId, string deviceProtocol)
    {
        DeviceId = deviceId;
        DeviceName = deviceName;
        ProtocolId = protocolId;
        DeviceProtocol = deviceProtocol;
    }
}
