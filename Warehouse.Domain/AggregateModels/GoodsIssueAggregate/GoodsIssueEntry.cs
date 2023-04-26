using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.AggregateModels.GoodsIssueAggregate;

public class GoodsIssueEntry : Entity
{
    public int GoodsIssueId { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public Employee Employee { get; private set; }
    public int TotalQuantity { get; private set; }
    public double TotalMass => (double)TotalQuantity / (double)Product.PiecesPerKilogram;
    public List<GoodsIssueEntryBasket> Baskets { get; private set; }
    public string? Note { get; private set; }

    private GoodsIssueEntry() { }

    public GoodsIssueEntry(int goodsIssueId, Product product, Employee employee, int totalQuantity): this()
    {
        GoodsIssueId = goodsIssueId;
        ProductId = product.Id;
        Product = product;
        Employee = employee;
        TotalQuantity = totalQuantity;
        Baskets = new List<GoodsIssueEntryBasket>();
    }

    public void AddBasket(Basket basket, int quantityToTake, double massToTake)
    {
        if (!basket.IsFullAndValid())
        {
            var exception = new WarehouseDomainException($"Basket with ID {basket.Id} is invalid.");
            exception.Data.Add("Basket", basket);
            throw exception;
        }

        #pragma warning disable CS8604 // Possible null reference argument.
        if (basket.Product != Product)
        #pragma warning restore CS8604 // Possible null reference argument.
        {
            throw new WarehouseDomainException($"Basket with ID {basket.Id}'s product ({basket.Product.Id}) isn't the same with goods issue entry's product ({Product.Id})");
        }

        if (basket.StorageSlot is null)
        {
            throw new WarehouseDomainException($"Basket with ID {basket.Id} currently isn't in stock.");
        }

        if (basket.ActualQuantity < quantityToTake)
        {
            throw new WarehouseDomainException($"Basket with ID {basket.Id} contain insufficient ammount of goods: {basket.ActualQuantity}. Required: {quantityToTake}");
        }

        GoodsIssueEntryBasket goodsIssueEntryBasket = new GoodsIssueEntryBasket(
            Id,
            basket.BasketId,
            basket.StorageSlot.StorageSlotId,
            quantityToTake,
            massToTake,
            #pragma warning disable CS8629 // Nullable value type may be null.
            basket.ProductionDate.Value);
            #pragma warning restore CS8629 // Nullable value type may be null.

        Baskets.Add(goodsIssueEntryBasket);
    }

    public void SetEmployee(Employee employee)
    {
        Employee = employee;
    }

    public void SetProduct(Product product)
    {
        Product = product;
    }
}
