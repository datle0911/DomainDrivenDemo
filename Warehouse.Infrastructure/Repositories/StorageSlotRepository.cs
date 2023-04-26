namespace Warehouse.Infrastructure.Repositories;

public class StorageSlotRepository : BaseRepository, IStorageSlotRepository
{
    public StorageSlotRepository(WarehouseDbContext context) : base(context)
    {
    }

    public StorageSlot Add(StorageSlot storageSlot)
    {
        if (storageSlot.IsTransient())
        {
            return _context.StorageSlots
                .Add(storageSlot)
                .Entity;
        }
        else
        {
            return storageSlot;
        }
    }

    public async Task<IEnumerable<StorageSlot>> GetAllAsync()
    {
        var storageSlots = await _context.StorageSlots
            .Include(s => s.Basket)
            .AsNoTracking()
            .ToListAsync();

        return storageSlots;
    }

    public StorageSlot Update(StorageSlot storageSlot)
    {
        return _context.StorageSlots
            .Update(storageSlot)
            .Entity;
    }
}
