namespace Warehouse.Infrastructure.EntityConfigurations;

public class GoodsIssueEntityTypeConfiguration : IEntityTypeConfiguration<GoodsIssue>
{
    public void Configure(EntityTypeBuilder<GoodsIssue> goodsIssueConfiguration)
    {
        goodsIssueConfiguration
            .HasKey(g => g.Id);
        goodsIssueConfiguration
            .Property(g => g.Id)
            .UseHiLo("goodsIssueeq", WarehouseDbContext.DEFAULT_SCHEMA);

        goodsIssueConfiguration
            .HasIndex(g => g.GoodsIssueId)
            .IsUnique();

        goodsIssueConfiguration
            .Ignore(g => g.DomainEvents);

        goodsIssueConfiguration
            .HasIndex(g => g.GoodsIssueId)
            .IsUnique();
        goodsIssueConfiguration
            .Property(g => g.GoodsIssueId)
            .IsRequired();

        goodsIssueConfiguration
            .HasMany(g => g.Entries)
            .WithOne()
            .HasForeignKey(g => g.GoodsIssueId)
            .IsRequired(false);

        goodsIssueConfiguration
            .HasOne(g => g.ShiftLeader)
            .WithMany()
            .IsRequired();

        goodsIssueConfiguration
            .Property(g => g.Timestamp)
            .IsRequired();
    }
}
