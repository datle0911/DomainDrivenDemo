namespace Warehouse.Infrastructure.EntityConfigurations;

public class StockCardEntryEntityTypeConfiguration : IEntityTypeConfiguration<StockCardEntry>
{
    public void Configure(EntityTypeBuilder<StockCardEntry> stockCardEntryConfiguration)
    {
        stockCardEntryConfiguration
            .HasKey(s => s.Id);
        stockCardEntryConfiguration
            .Property(s => s.Id)
            .UseHiLo("stockcardentryeq", WarehouseDbContext.DEFAULT_SCHEMA);

        stockCardEntryConfiguration
            .HasIndex(s => new { s.ProductId, s.Date })
            .IsUnique();

        stockCardEntryConfiguration
            .HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.Date)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.BeforeMass)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.BeforeQuantity)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.AfterMass)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.AfterQuantity)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.InputMass)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.InputQuantity)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.OutputMass)
            .IsRequired();

        stockCardEntryConfiguration
            .Property(s => s.OutputQuantity)
            .IsRequired();
    }
}
