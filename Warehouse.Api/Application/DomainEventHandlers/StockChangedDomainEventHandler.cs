namespace Warehouse.Api.Application.DomainEventHandlers;

public class StockChangedDomainEventHandler : INotificationHandler<StockChangedDomainEvent>
{
    private readonly IStockCardEntryRepository _stockCardEntryRepository;

    public StockChangedDomainEventHandler(IStockCardEntryRepository stockCardEntryRepository)
    {
        _stockCardEntryRepository = stockCardEntryRepository;
    }

    public async Task Handle(StockChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        StockCardEntry entry;

        var latestEntry = await _stockCardEntryRepository.GetLastestAsync(notification.StockChange.Product.Id, notification.StockChange.Date);

        if (latestEntry is null)
        {
            entry = new StockCardEntry(notification.StockChange.Product, notification.StockChange.Date);
            entry.UpdateEntry(notification.StockChange);
            _stockCardEntryRepository.Add(entry);
            return;
        }

        if (latestEntry.Date != notification.StockChange.Date)
        {
            entry = new StockCardEntry(notification.StockChange, latestEntry);
            _stockCardEntryRepository.Add(entry);
            return;
        }

        entry = latestEntry;
        entry.UpdateEntry(notification.StockChange);
        _stockCardEntryRepository.Update(entry);
    }
}
