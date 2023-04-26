using Warehouse.Api.Application.Commands.Baskets;

namespace Warehouse.Api.Application.Commands;

public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, bool>
{
    private readonly IBasketRepository _basketRepository;

    public CreateBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<bool> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = new Basket(request.BasketId);

        _basketRepository.Add(basket);

        var result = await _basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}
