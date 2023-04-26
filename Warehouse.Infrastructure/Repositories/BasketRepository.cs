namespace Warehouse.Infrastructure.Repositories;

public class BasketRepository : BaseRepository, IBasketRepository
{
    public BasketRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Basket Add(Basket basket)
    {
        if (basket.IsTransient())
        {
            return _context.Baskets
                .Add(basket)
                .Entity;
        }
        else
        {
            return basket;
        }
    }

    public async Task<IEnumerable<Basket>> GetAllAsync()
    {
        var baskets = await _context.Baskets
            .Include(b => b.Product)
            .Include(b => b.StorageSlot)
            .AsNoTracking()
            .ToListAsync();

        return baskets;
    }

    public async Task<Basket?> GetAsync(string basketId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Product)
            .Include(b => b.StorageSlot)
            .Where(b => b.BasketId == basketId)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        return basket;
    }

    public Basket Update(Basket basket)
    {
        return _context.Baskets
            .Update(basket)
            .Entity;
    }
}
