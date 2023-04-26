using Warehouse.Api.Application.Commands.GoodsReceipts;

namespace Warehouse.Api.Application.Commands;

public class CreateGoodsReceiptCommandHandler : IRequestHandler<CreateGoodsReceiptCommand, bool>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IProductRepository _productRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, IProductRepository productRepository, IBasketRepository basketRepository, IEmployeeRepository employeeRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _productRepository = productRepository;
        _basketRepository = basketRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(CreateGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetAsync(request.EmployeeId);
        if (employee is null)
        {
            return false;
        }

        var entries = new List<GoodsReceiptEntry>();

        foreach (var baskedId in request.BasketIds)
        {
            var basket = await _basketRepository.GetAsync(baskedId);
            if (basket is null)
            {
                return false;
            }

            if (!basket.IsFullAndValid())
            {
                return false;
            }

            var product = basket.Product;
            if (product is null)
            {
                return false;
            }

#pragma warning disable CS8629 // Nullable value type may be null.
            entries.Add(new GoodsReceiptEntry(product, basket.BasketId, basket.PlannedQuantity.Value, basket.ActualQuantity.Value, basket.ProductionDate.Value));
#pragma warning restore CS8629 // Nullable value type may be null.
        }

        var goodsReceipt = new GoodsReceipt(request.Timestamp, employee, entries);
        goodsReceipt.Confirm();

        _goodsReceiptRepository.Add(goodsReceipt);

        var result = await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}
