namespace Warehouse.Domain.Events;

public class StockChangedDomainEvent: INotification
{
    public StockChange StockChange { get; private set; }

    public StockChangedDomainEvent(StockChange stockChange)
    {
        StockChange = stockChange;
    }
}
