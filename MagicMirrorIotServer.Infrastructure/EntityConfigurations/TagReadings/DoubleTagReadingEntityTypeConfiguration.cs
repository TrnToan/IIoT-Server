using MagicMirrorIotServer.Domain.AggregateModels.Metrics;

namespace MagicMirrorIotServer.Infrastructure.EntityConfigurations.TagReadings;
public class DoubleTagReadingEntityTypeConfiguration : IEntityTypeConfiguration<TagReading<double>>
{
    public void Configure(EntityTypeBuilder<TagReading<double>> builder)
    {
        builder.HasKey(x => new { x.TagId, x.Timestamp });
    }
}
