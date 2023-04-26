namespace Warehouse.Api.Application.ViewModels;

public class AddGoodsIssuesEntryBasketViewModel
{
    public string BasketId { get; private set; }
    public int QuantityToTake { get; private set; }
    public double MassToTake { get; private set; }

    public AddGoodsIssuesEntryBasketViewModel(string basketId, int quantityToTake, double massToTake)
    {
        BasketId = basketId;
        QuantityToTake = quantityToTake;
        MassToTake = massToTake;
    }
}
