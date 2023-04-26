namespace Warehouse.Api.Application.ViewModels;

public class CreateGoodsIssueEntryViewModel
{
    public string EmployeeId { get; private set; }
    public string ProductId { get; private set; }
    public int TotalQuantity { get; private set; }

    public CreateGoodsIssueEntryViewModel(string employeeId, string productId, int totalQuantity)
    {
        EmployeeId = employeeId;
        ProductId = productId;
        TotalQuantity = totalQuantity;
    }
}
