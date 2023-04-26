namespace Warehouse.Api.Application.Commands.GoodsIssues;

public class CreateGoodsIssueCommandHandler : IRequestHandler<CreateGoodsIssueCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProductRepository _productRepository;

    public CreateGoodsIssueCommandHandler(IGoodsIssueRepository goodsIssueRepository, IEmployeeRepository employeeRepository, IProductRepository productRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _employeeRepository = employeeRepository;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(CreateGoodsIssueCommand request, CancellationToken cancellationToken)
    {
        var shiftLeader = await _employeeRepository.GetAsync(request.ShiftLeaderEmployeeId);
        if (shiftLeader is null)
        {
            return false;
        }

        var goodsIssue = new GoodsIssue(request.GoodsIssueId, request.Timestamp, shiftLeader);

        foreach(var entryViewModel in request.Entries)
        {
            var product = await _productRepository.GetAsync(entryViewModel.ProductId);
            if (product is null)
            {
                return false;
            }

            var employee = await _employeeRepository.GetAsync(entryViewModel.EmployeeId);
            if (employee is null)
            {
                return false;
            }

            goodsIssue.AddEntry(product, employee, entryViewModel.TotalQuantity);
        }

        _goodsIssueRepository.Add(goodsIssue);
        var result = await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync();

        return result;
    }
}
