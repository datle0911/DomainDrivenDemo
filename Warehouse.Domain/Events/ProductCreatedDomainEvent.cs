namespace Warehouse.Domain.Events;

public class ProductCreatedDomainEvent: INotification
{
    public Product Product { get; private set; }

    public ProductCreatedDomainEvent(Product product)
    {
        Product = product;
    }
}
