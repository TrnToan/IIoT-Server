namespace MagicMirrorIotServer.Domain.AggregateModels.Metrics;
public interface ITagReadingRepository
{
    public Task AddAsync(TagReading<object> tagReading);
    public Task<bool> IsTagExistedAsync(int tagId, DateTime timestamp);
    public Task<IEnumerable<TagReading<double>>> GetTagReadingsByTagId(int tagId, DateTime startTime, DateTime endTime);
}
