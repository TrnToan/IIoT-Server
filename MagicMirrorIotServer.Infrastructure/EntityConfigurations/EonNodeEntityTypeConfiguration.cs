namespace MagicMirrorIotServer.Infrastructure.EntityConfigurations;
public class EonNodeEntityTypeConfiguration : IEntityTypeConfiguration<EonNode>
{
    public void Configure(EntityTypeBuilder<EonNode> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("eonnodeeq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder
            .Ignore(x => x.DomainEvents);

        builder
            .HasAlternateKey(x => x.EonNodeId);

        builder
            .HasMany(x => x.Devices)
            .WithOne()
            .HasForeignKey(x => x.EonNodeId);

        builder
            .Property(x => x.EonNodeId)
            .HasMaxLength(40);
        builder
            .Property(x => x.EonNodeName)
            .HasMaxLength(100);
    }
}
