namespace Warehouse.Domain.AggregateModels.BasketAggregate;

public interface IBasketRepository: IRepository<Basket>
{
    Basket Add(Basket basket);
    Basket Update(Basket basket);
    Task<Basket?> GetAsync(string basketId);
    Task<IEnumerable<Basket>> GetAllAsync();
}
