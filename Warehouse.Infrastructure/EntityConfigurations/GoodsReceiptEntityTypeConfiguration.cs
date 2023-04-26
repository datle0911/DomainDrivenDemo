namespace Warehouse.Infrastructure.EntityConfigurations;

public class GoodsReceiptEntityTypeConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> goodsReceiptConfiguration)
    {
        goodsReceiptConfiguration
            .HasKey(g => g.Id);
        goodsReceiptConfiguration
            .Property(g => g.Id)
            .UseHiLo("goodsreceipteq", WarehouseDbContext.DEFAULT_SCHEMA);

        goodsReceiptConfiguration
            .Ignore(g => g.DomainEvents);

        goodsReceiptConfiguration
            .Property(g => g.Timestamp)
            .IsRequired();
        goodsReceiptConfiguration
            .HasIndex(g => g.Timestamp)
            .IsUnique();

        goodsReceiptConfiguration
            .HasMany(g => g.Entries)
            .WithOne()
            .HasForeignKey(g => g.GoodsReceiptId)
            .IsRequired();

        goodsReceiptConfiguration
            .HasOne(g => g.Employee)
            .WithMany()
            .IsRequired();
    }
}
