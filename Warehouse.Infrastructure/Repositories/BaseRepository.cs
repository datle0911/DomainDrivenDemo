namespace Warehouse.Infrastructure.Repositories;

public class BaseRepository
{
    protected readonly WarehouseDbContext _context;
    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public BaseRepository(WarehouseDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}
