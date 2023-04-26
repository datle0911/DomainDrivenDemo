namespace Warehouse.Domain.AggregateModels.ProductAggregate;

public class Product : Entity, IAggregateRoot
{
    public string ProductId { get; private set; }
    public string Name { get; private set; }
    public double PiecesPerKilogram { get; private set; }
    public EUnitOfMeasurement UnitOfMeasurement { get; private set; }

    public Product(string productId, string name, double piecesPerKilogram, EUnitOfMeasurement unitOfMeasurement)
    {
        ProductId = productId;
        Name = name;
        PiecesPerKilogram = piecesPerKilogram;
        UnitOfMeasurement = unitOfMeasurement;
    }

    public void UpdateInformation(string name, double piecesPerKilogram, EUnitOfMeasurement unitOfMeasurement)
    {
        Name = name;
        PiecesPerKilogram = piecesPerKilogram;
        UnitOfMeasurement = unitOfMeasurement;
    }

}
