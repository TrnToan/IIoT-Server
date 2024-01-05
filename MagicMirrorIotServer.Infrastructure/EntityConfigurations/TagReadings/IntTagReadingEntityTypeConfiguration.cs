using MagicMirrorIotServer.Domain.AggregateModels.Metrics;

namespace MagicMirrorIotServer.Infrastructure.EntityConfigurations.TagReadings;
public class IntTagReadingEntityTypeConfiguration : IEntityTypeConfiguration<TagReading<int>>
{
    public void Configure(EntityTypeBuilder<TagReading<int>> builder)
    {
        builder.HasKey(x => new { x.TagId, x.Timestamp });
    }
}
