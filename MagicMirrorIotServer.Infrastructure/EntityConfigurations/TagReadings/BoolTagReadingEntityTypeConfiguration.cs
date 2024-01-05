using MagicMirrorIotServer.Domain.AggregateModels.Metrics;

namespace MagicMirrorIotServer.Infrastructure.EntityConfigurations.TagReadings;
public class BoolTagReadingEntityTypeConfiguration : IEntityTypeConfiguration<TagReading<bool>>
{
    public void Configure(EntityTypeBuilder<TagReading<bool>> builder)
    {
        builder.HasKey(x => new { x.TagId, x.Timestamp });
    }
}
