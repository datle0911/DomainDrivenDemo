namespace Warehouse.Infrastructure.EntityConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productConfiguration)
    {
        productConfiguration
            .HasKey(p => p.Id);
        productConfiguration
            .Property(p => p.Id)
            .UseHiLo("producteq", WarehouseDbContext.DEFAULT_SCHEMA);

        productConfiguration
            .Ignore(p => p.DomainEvents);

        productConfiguration
            .HasIndex(p => p.ProductId)
            .IsUnique();

        productConfiguration
            .Property(p => p.ProductId)
            .HasMaxLength(40)
            .IsRequired();
        productConfiguration
            .HasIndex(p => p.ProductId)
            .IsUnique();

        productConfiguration
            .Property(p => p.Name);
    }
}

