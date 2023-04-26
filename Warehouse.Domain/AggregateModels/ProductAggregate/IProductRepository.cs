namespace Warehouse.Domain.AggregateModels.ProductAggregate;

public interface IProductRepository : IRepository<Product>
{
    Product Add(Product product);
    Product Update(Product product);
    Task<Product?> GetAsync(string productId);
    Task<IEnumerable<Product>> GetAllAsync();
}
