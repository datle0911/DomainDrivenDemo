namespace Warehouse.Domain.AggregateModels.StorageSlotAggregate;

public class StorageSlot : Entity, IAggregateRoot
{
    public string StorageSlotId { get; private set; }
    public Basket? Basket { get; private set; }
    public bool IsOccupied => Basket is not null;

    public StorageSlot(string storageSlotId)
    {
        StorageSlotId = storageSlotId;
    }
}
