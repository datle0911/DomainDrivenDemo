namespace Warehouse.Domain.AggregateModels.GoodsIssueAggregate;

public class GoodsIssue : Entity, IAggregateRoot
{
    public string GoodsIssueId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Employee ShiftLeader { get; private set; }
    public List<GoodsIssueEntry> Entries { get; private set; }

    private GoodsIssue() { }

    public GoodsIssue(string goodsIssueId, DateTime timestamp, Employee shiftLeader): this()
    {
        GoodsIssueId = goodsIssueId;
        Timestamp = timestamp;
        ShiftLeader = shiftLeader;
        Entries = new List<GoodsIssueEntry>();
    }

    public void AddEntry(Product product, Employee employee, int totalQuantity)
    {
        var entry = new GoodsIssueEntry(Id, product, employee, totalQuantity);
        foreach (var existedEntry in Entries)
        {
            if (entry.Product == existedEntry.Product)
            {
                throw new WarehouseDomainException($"Entry with product with ID {entry?.Product?.ProductId} is already in the goods issue.");
            }
        }

        Entries.Add(entry);
    }

    public void AddBasket(Basket basket, int quantityToTake, double massToTake)
    {
        if (!basket.IsFullAndValid())
        {
            return;
        }

        #pragma warning disable CS8604 // Possible null reference argument.
        var entryToAdd = Entries.Where(e => e.Product == basket?.Product).FirstOrDefault();
        #pragma warning restore CS8604 // Possible null reference argument.

        if (entryToAdd is null)
        {
            throw new WarehouseDomainException($"Product with ID {basket?.Product?.ProductId} isn't currently in the goods issue.");
        }

        entryToAdd.AddBasket(basket, quantityToTake, massToTake);
    }

    public void ExportBaskets(List<string> basketIds)
    {
        foreach (var basketId in basketIds)
        {
            ExportBasket(basketId);
        }
    }

    private void ExportBasket(string basketId)
    {
        var entry = Entries
            .Where(e => e.Baskets.Any(b => b.BasketId == basketId))
            .FirstOrDefault();

        if (entry is null)
        {
            throw new WarehouseDomainException($"Basket with ID {basketId} isn't currently in the goods issue.");
        }

        var basket = entry.Baskets
            .Where(b => b.BasketId == basketId)
            .FirstOrDefault();

        if (basket is null)
        {
            throw new WarehouseDomainException($"Basket with ID {basketId} isn't currently in the goods issue.");
        }

        basket.Export();

        var stockChange = new StockChange(entry.Product, Timestamp.Date, -basket.Quantity, -basket.Mass, "");
        this.AddDomainEvent(new BasketContentChangedDomainEvent(basketId, -basket.Quantity));
        this.AddDomainEvent(new StockChangedDomainEvent(stockChange));
    }
}
