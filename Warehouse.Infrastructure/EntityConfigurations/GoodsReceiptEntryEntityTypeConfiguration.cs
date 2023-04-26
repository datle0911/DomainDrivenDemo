namespace Warehouse.Infrastructure.EntityConfigurations;

public class GoodsReceiptEntryEntityTypeConfiguration : IEntityTypeConfiguration<GoodsReceiptEntry>
{
    public void Configure(EntityTypeBuilder<GoodsReceiptEntry> goodsReceiptEntryConfiguration)
    {
        goodsReceiptEntryConfiguration
            .HasKey(g => g.Id);
        goodsReceiptEntryConfiguration
            .Property(g => g.Id)
            .UseHiLo("goodsreceiptentryeq", WarehouseDbContext.DEFAULT_SCHEMA);

        goodsReceiptEntryConfiguration
            .Ignore(g => g.DomainEvents);

        goodsReceiptEntryConfiguration
            .HasIndex(g => new { g.GoodsReceiptId, g.BasketId })
            .IsUnique();

        goodsReceiptEntryConfiguration
            .Property(g => g.GoodsReceiptId)
            .IsRequired();
        goodsReceiptEntryConfiguration
            .Property(g => g.BasketId)
            .IsRequired();

        goodsReceiptEntryConfiguration
            .Property(g => g.PlannedQuantity)
            .IsRequired();

        goodsReceiptEntryConfiguration
            .Property(g => g.ProductionDate)
            .IsRequired();
    }
}
