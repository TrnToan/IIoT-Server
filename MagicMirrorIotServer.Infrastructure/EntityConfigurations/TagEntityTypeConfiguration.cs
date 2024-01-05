namespace MagicMirrorIotServer.Infrastructure.EntityConfigurations;
public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .UseHiLo("tageq", ApplicationDbContext.DEFAULT_SCHEMA);

        builder
            .Ignore(x => x.DomainEvents);

        builder
            .HasAlternateKey(x => new { x.TagId, x.DeviceId });

        builder
            .Property(x => x.TagId)
            .HasMaxLength(40);
        builder
            .Property(x => x.TagName)
            .HasMaxLength(200)
            .IsRequired();        
        builder
            .Property(x => x.TagType)
            .IsRequired();
    }
}
