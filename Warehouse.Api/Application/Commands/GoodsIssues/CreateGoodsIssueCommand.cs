namespace Warehouse.Api.Application.Commands.GoodsIssues;

[DataContract]
public class CreateGoodsIssueCommand: IRequest<bool>
{
    [DataMember]
    public string GoodsIssueId { get; private set; }
    [DataMember]
    public DateTime Timestamp { get; private set; }
    [DataMember]
    public string ShiftLeaderEmployeeId { get; private set; }
    [DataMember]
    public List<CreateGoodsIssueEntryViewModel> Entries { get; private set; }

    public CreateGoodsIssueCommand(string goodsIssueId, DateTime timestamp, string shiftLeaderEmployeeId, List<CreateGoodsIssueEntryViewModel> entries)
    {
        GoodsIssueId = goodsIssueId;
        Timestamp = timestamp;
        ShiftLeaderEmployeeId = shiftLeaderEmployeeId;
        Entries = entries;
    }
}
