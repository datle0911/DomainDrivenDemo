namespace Warehouse.Domain.AggregateModels.BasketAggregate;

public class Basket: Entity, IAggregateRoot
{
    public string BasketId { get; private set; }
    public Product? Product { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public int? StorageSlotId { get; private set; }
    public StorageSlot? StorageSlot { get; private set; }
    public int? PlannedQuantity { get; private set; }
    public int? ActualQuantity { get; private set; }
    public bool IsConsistent { get; private set; } = true;

    public double? PlannedMass => ((double?)PlannedQuantity) / Product?.PiecesPerKilogram;
    public double? ActualMass => ((double?)ActualQuantity) / Product?.PiecesPerKilogram;

    public Basket(string basketId)
    {
        BasketId = basketId;
    }

    public void UpdatePlannedInformation(int plannedQuantity, DateTime productionDate, Product product)
    {
        PlannedQuantity = plannedQuantity;
        ProductionDate = productionDate;
        Product = product;
    }

    public void UpdateActualQuantity(int actualQuantity)
    {
        ActualQuantity = actualQuantity;
    }

    public void UpdateStorageSlot(StorageSlot storageSlot)
    {
        if (storageSlot.Basket is not null)
        {
            StorageSlot = storageSlot;
        }
    }

    public void UpdateConsistency(bool isConsistent)
    {
        IsConsistent = isConsistent;
    }

    public void Clear()
    {
        PlannedQuantity = null;
        ProductionDate = null;
        Product = null;
        ActualQuantity = null;
        StorageSlot = null;
    }

    public bool IsFullAndValid()
    {
        bool valid = PlannedQuantity > 0 && ActualQuantity > 0 && Product is not null && ProductionDate is not null;

        return valid;
    }
}
