namespace MagicMirrorIotServer.Infrastructure.EntityConfigurations;
public class DeviceEntityTypeConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("deviceeq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder
            .Ignore(x => x.DomainEvents);

        builder
            .HasAlternateKey(x => new { x.DeviceId, x.EonNodeId });

        builder
            .HasMany(x => x.Tags)
            .WithOne()
            .HasForeignKey(x => x.DeviceId);

        builder
            .Property(x => x.DeviceId)
            .HasMaxLength(40);        
        builder
            .Property(x => x.DeviceName)
            .HasMaxLength(100);

        builder
            .Property(x => x.ProtocolId);
        builder
            .Property(x => x.DeviceProtocol);
    }
}
