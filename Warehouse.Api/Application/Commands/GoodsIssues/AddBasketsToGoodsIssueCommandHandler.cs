namespace Warehouse.Api.Application.Commands.GoodsIssues;

public class AddBasketsToGoodsIssueCommandHandler : IRequestHandler<AddBasketsToGoodsIssueCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IBasketRepository _basketRepository;

    public AddBasketsToGoodsIssueCommandHandler(IGoodsIssueRepository goodsIssueRepository, IBasketRepository basketRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _basketRepository = basketRepository;
    }

    public async Task<bool> Handle(AddBasketsToGoodsIssueCommand request, CancellationToken cancellationToken)
    {
        var goodsIssue = await _goodsIssueRepository.FindByIdAsync(request.GoodsIssueId);

        if (goodsIssue is null)
        {
            return false;
        }

        foreach (var basketViewModel in request.Baskets)
        {
            var basket = await _basketRepository.GetAsync(basketViewModel.BasketId);
            if (basket is null) 
            {
                return false;
            }

            goodsIssue.AddBasket(basket, basketViewModel.QuantityToTake, basketViewModel.MassToTake);
        }

        _goodsIssueRepository.Update(goodsIssue);
        var result = await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}
