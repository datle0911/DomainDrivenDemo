namespace Warehouse.Infrastructure.EntityConfigurations;

public class StorageSlotEntityTypeConfiguration : IEntityTypeConfiguration<StorageSlot>
{
    public void Configure(EntityTypeBuilder<StorageSlot> storageSlotConfiguration)
    {
        storageSlotConfiguration
            .HasKey(s => s.Id);
        storageSlotConfiguration
            .Property(s => s.Id)
            .UseHiLo("storagesloteq", WarehouseDbContext.DEFAULT_SCHEMA);

        storageSlotConfiguration
            .Ignore(s => s.DomainEvents);

        storageSlotConfiguration
            .HasIndex(s => s.StorageSlotId)
            .IsUnique();

        storageSlotConfiguration
            .Property(p => p.StorageSlotId)
            .HasMaxLength(40)
            .IsRequired();
        storageSlotConfiguration
            .HasIndex(p => p.StorageSlotId)
            .IsUnique();
    }
}
