using Warehouse.Api.Application.Commands.Products;

namespace Warehouse.Api.Application.Commands;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand,bool>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.ProductId,request.Name,request.PiecesPerKilogram,request.UnitOfMeasurement);

        _repository.Add(product);

        return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
