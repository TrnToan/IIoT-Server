namespace MagicMirrorIotServer.Domain.AggregateModels.EonNodeAggregate;
public class Device: Entity
{
    public Device(string deviceId, string deviceName, string protocolId, string deviceProtocol)
    {
        DeviceId = deviceId;
        DeviceName = deviceName;
        ProtocolId = protocolId;
        DeviceProtocol = deviceProtocol;
        Tags = new List<Tag>();
    }

    public int EonNodeId { get; private set; }
    public string DeviceId { get; private set; }
    public string DeviceName { get; private set; }
    public string ProtocolId { get; private set; }
    public string DeviceProtocol { get; private set; }
    public List<Tag> Tags { get; private set; }

    public void AddTag(string tagId, string tagName, DateTime timestamp, string tagValue, TagType tagType)
    {
        var tag = new Tag(tagId, tagName, timestamp, tagValue, tagType);
        if (Tags.Exists(t => t.TagId == tagId))
        {
            throw new ChildEntityDuplicationException(tagId, tag, DeviceId, this);
        }

        Tags.Add(tag);
    }

    public void RemoveTag(string tagId)
    {
        var tag = Tags.Find(t => t.TagId == tagId);
        if (tag is null)
        {
            throw new ChildEntityNotFoundException(tagId, typeof(Tag), DeviceId, nameof(Device));
        }

        Tags.Remove(tag);
    }
}
