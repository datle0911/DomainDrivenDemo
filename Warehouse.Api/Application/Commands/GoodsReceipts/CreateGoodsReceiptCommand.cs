namespace Warehouse.Api.Application.Commands.GoodsReceipts;

[DataContract]
public class CreateGoodsReceiptCommand : IRequest<bool>
{
    [DataMember]
    public DateTime Timestamp { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public List<string> BasketIds { get; private set; }

    public CreateGoodsReceiptCommand(DateTime timestamp, string employeeId, List<string> basketIds)
    {
        Timestamp = timestamp;
        EmployeeId = employeeId;
        BasketIds = basketIds;
    }
}
