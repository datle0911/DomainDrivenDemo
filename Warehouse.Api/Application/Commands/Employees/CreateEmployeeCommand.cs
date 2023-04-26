namespace Warehouse.Api.Application.Commands.Employees;

[DataContract]
public class CreateEmployeeCommand : IRequest<bool>
{
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public string FirstName { get; private set; }
    [DataMember]
    public string LastName { get; private set; }
    [DataMember]
    public DateTime DateOfBirth { get; private set; }

    public CreateEmployeeCommand(string employeeId, string firstName, string lastName, DateTime dateOfBirth)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }
}
