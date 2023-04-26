namespace Warehouse.Api.Application.Commands.Baskets;

[DataContract]
public class CreateBasketCommand : IRequest<bool>
{
    [DataMember]
    public string BasketId { get; private set; }

    public CreateBasketCommand(string basketId)
    {
        BasketId = basketId;
    }
}
