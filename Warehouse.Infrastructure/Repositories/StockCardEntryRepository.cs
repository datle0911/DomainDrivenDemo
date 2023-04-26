namespace Warehouse.Infrastructure.Repositories;

public class StockCardEntryRepository : BaseRepository, IStockCardEntryRepository
{
    public StockCardEntryRepository(WarehouseDbContext context) : base(context)
    {
    }

    public StockCardEntry Add(StockCardEntry entry)
    {
        if (entry.IsTransient())
        {
            return _context.StockCardEntries
                .Add(entry)
                .Entity;
        }
        else
        {
            return entry;
        }
    }

    public async Task<IEnumerable<StockCardEntry>> GetByProductAsync(int productId)
    {
        var stockCardEntries = await _context.StockCardEntries
            .Where(x => x.ProductId == productId)
            .Include(s => s.Product)
            .AsNoTracking()
            .ToListAsync();

        return stockCardEntries;
    }

    public async Task<StockCardEntry?> GetLastestAsync(int productId, DateTime date)
    {
        var stockCardEntry = await _context.StockCardEntries
            .Where(x => x.ProductId == productId)
            .OrderBy(x => x.Date)
            .Include(s => s.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return stockCardEntry;
    }

    public StockCardEntry Update(StockCardEntry entry)
    {
        var trackedProduct = _context.ChangeTracker
            .Entries<Product>()
            .Where(p => p.Entity == entry.Product)
            .FirstOrDefault()?
            .Entity;

        if (trackedProduct is not null)
        {
            entry.SetProduct(trackedProduct);
        }

        return _context.StockCardEntries
            .Update(entry)
            .Entity;
    }
}
