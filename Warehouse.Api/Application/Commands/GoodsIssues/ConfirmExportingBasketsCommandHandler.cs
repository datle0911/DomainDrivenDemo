namespace Warehouse.Api.Application.Commands.GoodsIssues;

public class ConfirmExportingBasketsCommandHandler : IRequestHandler<ConfirmExportingBasketsCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;

    public ConfirmExportingBasketsCommandHandler(IGoodsIssueRepository goodsIssueRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
    }

    public async Task<bool> Handle(ConfirmExportingBasketsCommand request, CancellationToken cancellationToken)
    {
        var goodsIssue = await _goodsIssueRepository.FindByIdAsync(request.GoodsIssueId);

        if (goodsIssue is null)
        {
            return false;
        }

        goodsIssue.ExportBaskets(request.BasketIds);

        _goodsIssueRepository.Update(goodsIssue);
        var result = await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}
