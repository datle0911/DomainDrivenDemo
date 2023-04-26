namespace Warehouse.Api.Application.Commands.GoodsIssues;

[DataContract]
public class AddBasketsToGoodsIssueCommand: IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public List<AddGoodsIssuesEntryBasketViewModel> Baskets { get; private set; }

    public AddBasketsToGoodsIssueCommand(string goodsIssueId, List<AddGoodsIssuesEntryBasketViewModel> baskets)
    {
        GoodsIssueId = goodsIssueId;
        Baskets = baskets;
    }
}
