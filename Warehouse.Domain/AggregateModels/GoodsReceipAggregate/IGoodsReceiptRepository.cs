namespace Warehouse.Domain.AggregateModels.GoodsReceipAggregate;

public interface IGoodsReceiptRepository : IRepository<GoodsReceipt>
{
    GoodsReceipt Add(GoodsReceipt goodsReceipt);
    Task<IEnumerable<GoodsReceipt>> GetListAsync(DateTime startTime, DateTime endTime);
}
