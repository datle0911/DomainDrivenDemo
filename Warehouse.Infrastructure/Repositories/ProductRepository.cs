namespace Warehouse.Infrastructure.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Product Add(Product product)
    {
        if (product.IsTransient())
        {
            return _context.Products
                .Add(product)
                .Entity;
        }
        else
        {
            return product;
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .ToListAsync();

        return products;
    }

    public async Task<Product?> GetAsync(string productId)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Where(p => p.ProductId == productId)
            .SingleOrDefaultAsync();

        return product;
    }

    public Product Update(Product product)
    {
        return _context.Products
            .Update(product)
            .Entity;
    }
}
