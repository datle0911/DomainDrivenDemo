namespace Warehouse.Api.Application.Commands.GoodsIssues;

[DataContract]
public class ConfirmExportingBasketsCommand: IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public List<string> BasketIds { get; private set; }

    public ConfirmExportingBasketsCommand(string goodsIssueId, List<string> basketIds)
    {
        GoodsIssueId = goodsIssueId;
        BasketIds = basketIds;
    }
}
