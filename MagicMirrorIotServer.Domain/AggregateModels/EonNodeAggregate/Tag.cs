namespace MagicMirrorIotServer.Domain.AggregateModels.EonNodeAggregate;
public class Tag : Entity
{
    public Tag(string tagId, string tagName, DateTime timestamp, string tagValue, TagType tagType)
    {
        TagId = tagId;
        TagName = tagName;
        Timestamp = timestamp;
        TagValue = tagValue;
        TagType = tagType;
    }

    public int DeviceId { get; private set; }
    public string TagId { get; private set; }
    public string TagName { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string TagValue { get; private set; }
    public TagType TagType { get; private set; }
}
