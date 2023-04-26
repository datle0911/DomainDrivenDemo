using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.AggregateModels.StockCardAggregate;

public class StockChange
{
    public Product Product { get; private set; }
    public DateTime Date { get; private set; }
    public int Quantity { get; private set; }
    public double Mass { get; private set; }
    public string Note { get; private set; }

    public StockChange(Product product, DateTime date, int quantity, double mass, string note)
    {
        Product = product;
        Date = date;
        Quantity = quantity;
        Mass = mass;
        Note = note;
    }
}
