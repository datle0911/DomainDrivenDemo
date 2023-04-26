namespace Warehouse.Domain.AggregateModels.StockCardAggregate;

public class StockCardEntry : Entity, IAggregateRoot
{
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public DateTime Date { get; private set; }
    public int BeforeQuantity { get; private set; }
    public double BeforeMass { get; private set; }
    public int InputQuantity { get; private set; }
    public double InputMass { get; private set; }
    public int OutputQuantity { get; private set; }
    public double OutputMass { get; private set; }
    public int AfterQuantity { get; private set; }
    public double AfterMass { get; private set; }
    public string Note { get; private set; }

    private StockCardEntry() { }

    public StockCardEntry(Product product, DateTime date)
    {
        Product = product;
        ProductId = product.Id;
        Date = date.Date;
        BeforeQuantity = 0;
        BeforeMass = 0;
        AfterQuantity = 0;
        AfterMass = 0;
        InputQuantity = 0;
        InputMass = 0;
        OutputQuantity = 0;
        OutputMass = 0;
        Note = "";
    }

    public StockCardEntry(StockChange stockChange, StockCardEntry latestEntry)
    {
        Product = stockChange.Product;
        Date = stockChange.Date.Date;
        BeforeQuantity = latestEntry.AfterQuantity;
        BeforeMass = latestEntry.BeforeMass;
        if (stockChange.Quantity > 0)
        {
            InputQuantity = stockChange.Quantity;
            OutputQuantity = 0;
            InputMass = stockChange.Mass;
            OutputMass = 0;
        }
        else
        {
            InputQuantity = 0;
            OutputQuantity = -stockChange.Quantity;
            InputMass = 0;
            OutputMass = -stockChange.Mass;
        }
        AfterQuantity = latestEntry.AfterQuantity + stockChange.Quantity;
        AfterMass = latestEntry.AfterMass + stockChange.Mass;
        Note = stockChange.Note;
    }

    public void UpdateEntry(StockChange stockChange)
    {
        if (stockChange.Product != Product || stockChange.Date.Date != Date)
        {
            throw new WarehouseDomainException($"Stock change's product isn't the same with stock card's product.");
        }

        if (stockChange.Quantity > 0)
        {
            InputQuantity += stockChange.Quantity;
            InputMass += stockChange.Mass;
        }
        else
        {
            OutputQuantity -= stockChange.Quantity;
            OutputMass -= stockChange.Mass;
        }

        AfterQuantity += stockChange.Quantity;
        AfterMass += stockChange.Mass;
        Note += stockChange.Note;
    }

    public void SetProduct(Product product)
    {
        Product = product;
        ProductId = product.Id;
    }
}
