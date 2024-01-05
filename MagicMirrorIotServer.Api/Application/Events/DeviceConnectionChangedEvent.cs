namespace MagicMirrorIotServer.Api.Application.Events;

public class DeviceConnectionChangedEvent: INotification
{
    public string EonNodeId { get; set; }
    public string DeviceId { get; set; }
    public bool IsConnected { get; set; }

    public DeviceConnectionChangedEvent(string eonNodeId, string deviceId, bool isConnected)
    {
        EonNodeId = eonNodeId;
        DeviceId = deviceId;
        IsConnected = isConnected;
    }
}
