namespace Warehouse.Api.Application.DomainEventHandlers;

public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
{
    private readonly IStockCardEntryRepository _stockCardEntryRepository;

    public ProductCreatedDomainEventHandler(IStockCardEntryRepository stockCardEntryRepository)
    {
        _stockCardEntryRepository = stockCardEntryRepository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var product = notification.Product;

        var stockCardEntry = new StockCardEntry(product, DateTime.Now);

        _stockCardEntryRepository.Add(stockCardEntry);
    }
}
