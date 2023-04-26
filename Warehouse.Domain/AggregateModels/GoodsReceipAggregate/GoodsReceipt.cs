namespace Warehouse.Domain.AggregateModels.GoodsReceipAggregate;

public class GoodsReceipt : Entity, IAggregateRoot
{
    public DateTime Timestamp { get; private set; }
    public Employee Employee { get; private set; }
    public List<GoodsReceiptEntry> Entries { get; private set; }

    private GoodsReceipt() { }

    public GoodsReceipt(DateTime timestamp, Employee employee, List<GoodsReceiptEntry> entries)
    {
        Timestamp = timestamp;
        Employee = employee;
        Entries = entries;
    }

    public void Confirm()
    {
        foreach (var entry in Entries)
        {
            var stockChange = new StockChange(entry.Product, Timestamp.Date, entry.ActualQuantity, entry.ActualMass, "");
            this.AddDomainEvent(new StockChangedDomainEvent(stockChange));
        }
    }
}
