using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.AggregateModels.GoodsIssueAggregate;

public class GoodsIssueEntryBasket: Entity
{
    public int GoodsIssueEntryId { get; private set; }
    public string BasketId { get; private set; }
    public string StorageSlotId { get; private set; }
    public int Quantity { get; private set; }
    public double Mass { get; private set; }
    public DateTime ProductionDate { get; private set; }
    public bool IsTaken { get; private set; } = false;

    private GoodsIssueEntryBasket() { }

    public GoodsIssueEntryBasket(int goodsIssueEntryId, string basketId, string storageSlotId, int quantity, double mass, DateTime productionDate)
    {
        GoodsIssueEntryId = goodsIssueEntryId;
        BasketId = basketId;
        StorageSlotId = storageSlotId;
        Quantity = quantity;
        Mass = mass;
        ProductionDate = productionDate;
    }

    public void Export()
    {
        IsTaken = true;
    }
}
