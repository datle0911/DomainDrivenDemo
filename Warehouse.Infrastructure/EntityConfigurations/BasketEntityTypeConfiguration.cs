namespace Warehouse.Infrastructure.EntityConfigurations;

public class BasketEntityTypeConfiguration: IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> basketConfiguration)
    {
        basketConfiguration
            .HasKey(b => b.Id);
        basketConfiguration
            .Property(b => b.Id)
            .UseHiLo("basketeq", WarehouseDbContext.DEFAULT_SCHEMA);

        basketConfiguration
            .Ignore(b => b.DomainEvents);

        basketConfiguration
            .HasIndex(b => b.BasketId)
            .IsUnique();

        basketConfiguration
            .Property(b => b.BasketId)
            .HasMaxLength(60)
            .IsRequired();
        basketConfiguration
            .HasIndex(b => b.BasketId)
            .IsUnique();

        basketConfiguration
            .HasOne(b => b.StorageSlot)
            .WithOne(s => s.Basket)
            .HasForeignKey<Basket>(b => b.StorageSlotId)
            .IsRequired(false);

        basketConfiguration
            .HasOne(b => b.Product)
            .WithMany();
    }
}
