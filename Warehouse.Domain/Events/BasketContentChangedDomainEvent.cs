namespace Warehouse.Domain.Events;

public class BasketContentChangedDomainEvent: INotification
{
    public string BaskedId { get; private set; }
    public int ChangedQuantity { get; private set; }

    public BasketContentChangedDomainEvent(string baskedId, int changedQuantity)
    {
        BaskedId = baskedId;
        ChangedQuantity = changedQuantity;
    }
}
