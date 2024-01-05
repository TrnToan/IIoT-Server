namespace MagicMirrorIotServer.Api.Application.Events;

public class TagValueChangedEvent : INotification
{
    public string EonNodeId { get; set; }
    public string DeviceId { get; set; }
    public string TagId { get; set; }
    public object Value { get; set; }
    public DateTime Timestamp { get; set; }

    public TagValueChangedEvent(string eonNodeId, string deviceId, string tagId, object value, DateTime timestamp)
    {
        EonNodeId = eonNodeId;
        DeviceId = deviceId;
        TagId = tagId;
        Value = value;
        Timestamp = timestamp;
    }
}
