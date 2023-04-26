namespace Warehouse.Infrastructure.Repositories;

public class BasketInconsistencyRepository : BaseRepository, IBasketInconsistencyRepository
{
    public BasketInconsistencyRepository(WarehouseDbContext context) : base(context)
    {
    }

    public BasketInconsistency Add(BasketInconsistency basketInconsistency)
    {
        if (basketInconsistency.IsTransient())
        {
            return _context.BasketInconsistencies
                .Add(basketInconsistency)
                .Entity;
        }
        else
        {
            return basketInconsistency;
        }
    }

    public async Task<BasketInconsistency?> FindAsync(string basketId, DateTime timestamp)
    {
        var basketInconsistency = await _context.BasketInconsistencies
            .Include(b => b.Product)
            .Where(b => b.BasketId == basketId && b.Timestamp == timestamp)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        return basketInconsistency;
    }

    public async Task<IEnumerable<BasketInconsistency>> GetUnfixedAsync()
    {
        var basketInconsistencies = await _context.BasketInconsistencies
            .Where(b => b.IsFixed == false)
            .AsNoTracking()
            .ToListAsync();

        return basketInconsistencies;
    }

    public BasketInconsistency Update(BasketInconsistency basketInconsistency)
    {
        return _context.BasketInconsistencies
                .Update(basketInconsistency)
                .Entity;
    }
}
