namespace Warehouse.Domain.AggregateModels.StockCardAggregate;

public interface IStockCardEntryRepository : IRepository<StockCardEntry>
{
    StockCardEntry Add(StockCardEntry entry);
    StockCardEntry Update(StockCardEntry entry);
    Task<IEnumerable<StockCardEntry>> GetByProductAsync(int productId);
    Task<StockCardEntry?> GetLastestAsync(int productId, DateTime date);
}
