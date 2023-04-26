using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.AggregateModels.GoodsReceipAggregate;

public class GoodsReceiptEntry : Entity
{
    public int GoodsReceiptId { get; private set; }
    public Product Product { get; set; }
    public string BasketId { get; set; }
    public int PlannedQuantity { get; set; }
    public int ActualQuantity { get; set; }
    public double PlannedMass => (this.PlannedQuantity / this.Product.PiecesPerKilogram);
    public double ActualMass => ((this.ActualQuantity / this.Product.PiecesPerKilogram));
    public DateTime ProductionDate { get; set; }

    private GoodsReceiptEntry() { }

    public GoodsReceiptEntry(Product product, string basketId, int plannedQuantity, int actualQuantity, DateTime productionDate)
    {
        Product = product;
        BasketId = basketId;
        PlannedQuantity = plannedQuantity;
        ActualQuantity = actualQuantity;
        ProductionDate = productionDate;
    }
}
