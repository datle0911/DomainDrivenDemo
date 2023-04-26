namespace Warehouse.Api.Application.Commands.Products;

[DataContract]
public class CreateProductCommand : IRequest<bool>
{
    [DataMember]
    public string ProductId { get; private set; }
    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public double PiecesPerKilogram { get; private set; }
    [DataMember]
    public EUnitOfMeasurement UnitOfMeasurement { get; private set; }

    public CreateProductCommand(string productId, string name, double piecesPerKilogram, EUnitOfMeasurement unitOfMeasurement)
    {
        ProductId = productId;
        Name = name;
        PiecesPerKilogram = piecesPerKilogram;
        UnitOfMeasurement = unitOfMeasurement;
    }
}
