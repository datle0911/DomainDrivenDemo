namespace Warehouse.Infrastructure.EntityConfigurations;

public class BasketInconsistencyEntityTypeConfiguration : IEntityTypeConfiguration<BasketInconsistency>
{
    public void Configure(EntityTypeBuilder<BasketInconsistency> basketInconsistencyConfiguration)
    {
        basketInconsistencyConfiguration
            .HasKey(b => b.Id);
        basketInconsistencyConfiguration
            .Property(b => b.Id)
            .UseHiLo("basketInconsistencieeq", WarehouseDbContext.DEFAULT_SCHEMA);

        basketInconsistencyConfiguration
            .HasIndex(b => new { b.BasketId, b.Timestamp});

        basketInconsistencyConfiguration
            .Property(b => b.BasketId)
            .IsRequired();

        basketInconsistencyConfiguration
            .Property(b => b.Timestamp)
            .IsRequired();

        basketInconsistencyConfiguration
           .HasOne(b => b.Product)
           .WithMany()
           .IsRequired();

        basketInconsistencyConfiguration
           .Property(b => b.GoodsIssueId)
           .IsRequired();

        basketInconsistencyConfiguration
           .Property(b => b.CurrentMass)
           .IsRequired();

        basketInconsistencyConfiguration
           .Property(b => b.CurrentQuantity)
           .IsRequired();

        basketInconsistencyConfiguration
           .HasOne(b => b.Reporter)
           .WithMany()
           .IsRequired();
    }
}
