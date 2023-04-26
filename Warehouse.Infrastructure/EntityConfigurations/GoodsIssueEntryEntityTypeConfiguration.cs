namespace Warehouse.Infrastructure.EntityConfigurations;

public class GoodsIssueEntryEntityTypeConfiguration : IEntityTypeConfiguration<GoodsIssueEntry>
{
    public void Configure(EntityTypeBuilder<GoodsIssueEntry> goodsIssueEntryConfiguration)
    {
        goodsIssueEntryConfiguration
            .HasKey(g=> g.Id);
        goodsIssueEntryConfiguration
            .Property(g => g.Id)
            .UseHiLo("goodsIssueEntriyeq", WarehouseDbContext.DEFAULT_SCHEMA);

        goodsIssueEntryConfiguration
            .Ignore(g => g.DomainEvents);

        goodsIssueEntryConfiguration
            .HasIndex(g => new {g.GoodsIssueId, g.ProductId})
            .IsUnique();

        goodsIssueEntryConfiguration
            .Property(g => g.GoodsIssueId)
            .IsRequired();

        goodsIssueEntryConfiguration
            .HasOne(g => g.Product)
            .WithMany()
            .HasForeignKey(g => g.ProductId)
            .IsRequired();

        goodsIssueEntryConfiguration
            .HasOne(g => g.Employee)
            .WithMany()
            .IsRequired();

        goodsIssueEntryConfiguration
            .Property(g => g.TotalQuantity)
            .IsRequired();

        goodsIssueEntryConfiguration
            .Property(g => g.TotalMass)
            .IsRequired();

        goodsIssueEntryConfiguration
            .Ignore(g => g.TotalMass);
    }
}
