using Warehouse.Api.Application.Commands.Employees;

namespace Warehouse.Api.Application.Commands;

public class CreateEmployeeCommandHandler: IRequestHandler<CreateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(request.EmployeeId, request.FirstName, request.LastName, request.DateOfBirth);

        _employeeRepository.Add(employee);

        var result = await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}
