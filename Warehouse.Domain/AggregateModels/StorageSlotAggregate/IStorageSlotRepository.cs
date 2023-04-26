namespace Warehouse.Domain.AggregateModels.StorageSlotAggregate;

public interface IStorageSlotRepository: IRepository<StorageSlot>
{
    StorageSlot Add(StorageSlot storageSlot);
    StorageSlot Update(StorageSlot storageSlot);
    Task <IEnumerable<StorageSlot>> GetAllAsync();
}
