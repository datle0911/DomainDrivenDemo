namespace Warehouse.Domain.AggregateModels.BasketInconsistencyAggregate;

public interface IBasketInconsistencyRepository
{
    BasketInconsistency Add(BasketInconsistency basketInconsistency);
    BasketInconsistency Update(BasketInconsistency basketInconsistency);
    Task<IEnumerable<BasketInconsistency>> GetUnfixedAsync();
    Task<BasketInconsistency?> FindAsync(string basketId, DateTime timestamp);
}
