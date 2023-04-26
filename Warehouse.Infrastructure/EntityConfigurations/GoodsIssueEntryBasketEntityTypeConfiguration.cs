namespace Warehouse.Infrastructure.EntityConfigurations;

public class GoodsIssueEntryBasketEntityTypeConfiguration : IEntityTypeConfiguration<GoodsIssueEntryBasket>
{
    public void Configure(EntityTypeBuilder<GoodsIssueEntryBasket> goodsIssueEntryBasketConfiguration)
    {
        goodsIssueEntryBasketConfiguration
            .HasKey(g => g.Id);
        goodsIssueEntryBasketConfiguration
            .Property(g => g.Id)
            .UseHiLo("goodsissueentrybasketeq", WarehouseDbContext.DEFAULT_SCHEMA);

        goodsIssueEntryBasketConfiguration
            .Ignore(g => g.DomainEvents);

        goodsIssueEntryBasketConfiguration
            .HasIndex(g => new { g.GoodsIssueEntryId, g.BasketId })
            .IsUnique();

        goodsIssueEntryBasketConfiguration
            .Property(g => g.GoodsIssueEntryId)
            .IsRequired();

        goodsIssueEntryBasketConfiguration
            .Property(g => g.BasketId)
            .IsRequired();

        goodsIssueEntryBasketConfiguration
            .Property(g => g.Mass)
            .IsRequired();

        goodsIssueEntryBasketConfiguration
            .Property(g => g.Quantity)
            .IsRequired();

        goodsIssueEntryBasketConfiguration
            .Property(g => g.ProductionDate)
            .IsRequired();

        goodsIssueEntryBasketConfiguration
            .Property(g => g.StorageSlotId)
            .IsRequired();

        goodsIssueEntryBasketConfiguration
            .Property(g => g.IsTaken)
            .IsRequired();
    }
}
