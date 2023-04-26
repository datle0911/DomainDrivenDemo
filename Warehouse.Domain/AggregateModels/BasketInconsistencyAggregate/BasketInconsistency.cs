namespace Warehouse.Domain.AggregateModels.BasketInconsistencyAggregate;

public class BasketInconsistency : Entity, IAggregateRoot
{
    public DateTime Timestamp { get; private set; }
    public string BasketId { get; private set; }
    public string StorageSlotId { get; private set; }
    public Product Product { get; private set; }
    public string GoodsIssueId { get; private set; }
    public int CurrentQuantity { get; private set; }
    public double CurrentMass { get; private set; }
    public int NewQuantity { get; private set; }
    public double NewMass { get; private set; }
    public string Note { get; private set; }
    public bool IsFixed { get; private set; }
    public Employee Reporter { get; private set; }

    private BasketInconsistency() { }

    public BasketInconsistency(
        string basketId,
        string storageSlotId,
        Product product,
        string goodsIssueId,
        int currentQuantity,
        double currentMass,
        Employee reporter)
    {
        BasketId = basketId;
        StorageSlotId = storageSlotId;
        Product = product;
        GoodsIssueId = goodsIssueId;
        CurrentQuantity = currentQuantity;
        CurrentMass = currentMass;
        Reporter = reporter;
        Note = "";

        this.AddDomainEvent(new BasketConsistencyChangedDomainEvent(basketId, false));
    }

    public void Fix(DateTime timestamp, int newQuantity, double newMass, string note)
    {
        Timestamp = timestamp;
        NewQuantity = newQuantity;
        NewMass = newMass;
        Note = note;

        var changedQuantity = newQuantity - CurrentQuantity;
        this.AddDomainEvent(new BasketContentChangedDomainEvent(BasketId, changedQuantity));
        this.AddDomainEvent(new BasketConsistencyChangedDomainEvent(BasketId, true));
    }
}
