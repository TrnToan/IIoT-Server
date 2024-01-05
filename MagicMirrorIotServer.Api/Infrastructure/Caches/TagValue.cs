namespace MagicMirrorIotServer.Api.Infrastructure.Caches;

public class TagValue<TValue>
{
    public string EonNodeId { get; set; }
    public string DeviceId { get; set; }
    public string TagId { get; set; }
    public TValue? Value { get; set; }

    public TagValue(string eonNodeId, string deviceId, string tagId, TValue? value)
    {
        EonNodeId = eonNodeId;
        DeviceId = deviceId;
        TagId = tagId;
        Value = value;
    }
}