namespace Warehouse.Infrastructure.Repositories;

public class GoodsReceiptRepository : BaseRepository, IGoodsReceiptRepository
{
    public GoodsReceiptRepository(WarehouseDbContext context) : base(context)
    {
    }

    public GoodsReceipt Add(GoodsReceipt goodsReceipt)
    {
        if (goodsReceipt.IsTransient())
        {
            _context.GoodsReceipts
                .Add(goodsReceipt);

            _context.Entry(goodsReceipt.Employee).State = EntityState.Unchanged;
            foreach (var entry in goodsReceipt.Entries)
            {
                _context.Entry(entry.Product).State = EntityState.Unchanged;
            }

            return goodsReceipt;
        }
        else
        {
            return goodsReceipt;
        }
    }

    public async Task<IEnumerable<GoodsReceipt>> GetListAsync(DateTime startTime, DateTime endTime)
    {
        var queryable = _context.GoodsReceipts
            .Include(g => g.Employee)
            .Include(g => g.Entries)
            .ThenInclude(g => g.Product)
            .Include(g => g.Employee)
            .AsNoTracking();

        queryable = queryable.Where(g =>
                g.Timestamp.CompareTo(startTime) > 0 &&
                g.Timestamp.CompareTo(endTime) < 0);

        var goodsReceipts = await queryable.ToListAsync();

        return goodsReceipts;
    }
}
