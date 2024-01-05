using MagicMirrorIotServer.Domain.AggregateModels.Metrics;

namespace MagicMirrorIotServer.Infrastructure.Repositories;
public class TagReadingRepository : BaseRepository, ITagReadingRepository
{
    public TagReadingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddAsync(TagReading<object> tagReading)
    {
        if (tagReading.Value is bool boolValue)
        {
            var boolTagReading = new TagReading<bool>(tagReading.TagId, boolValue, tagReading.Timestamp);
            await _context.BoolTagReadings.AddAsync(boolTagReading);
        }

        if (tagReading.Value is double doubleValue)
        {
            var doubleTagReading = new TagReading<double>(tagReading.TagId, doubleValue, tagReading.Timestamp);
            await _context.DoubleTagReadings.AddAsync(doubleTagReading);
        }

        if (tagReading.Value is int intValue)
        {
            var intTagReading = new TagReading<int>(tagReading.TagId, intValue, tagReading.Timestamp);
            await _context.IntTagReadings.AddAsync(intTagReading);
        }
    }

    public async Task<bool> IsTagExistedAsync(int tagId, DateTime timestamp)
    {
        var boolTagReading = await _context.BoolTagReadings.FindAsync(tagId, timestamp);
        if (boolTagReading is not null)
        {
            return true;
        }

        var intTagReading = await _context.IntTagReadings.FindAsync(tagId, timestamp);
        if (intTagReading is not null)
        {
            return true;
        }

        var floatTagReading = await _context.DoubleTagReadings.FindAsync(tagId, timestamp);
        if (floatTagReading is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<TagReading<double>>> GetTagReadingsByTagId(int tagId, DateTime startTime, DateTime endTime)
    {
        var floatTagReadings = await _context.DoubleTagReadings
            .Where(tag => tag.TagId == tagId)
            .Where(tag => tag.Timestamp >= startTime && tag.Timestamp <= endTime)
            .ToListAsync();

        return floatTagReadings;
    }
}
