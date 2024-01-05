namespace MagicMirrorIotServer.Domain.AggregateModels.Metrics;
public class TagReading<TValue>: IAggregateRoot
{
    public int TagId { get; private set; }
    public TValue Value { get; private set; }
    public DateTime Timestamp { get; private set; }
    
    public TagReading(int tagId, TValue value, DateTime timestamp)
    {
        TagId = tagId;
        Value = value;
        Timestamp = timestamp;
    }
}
