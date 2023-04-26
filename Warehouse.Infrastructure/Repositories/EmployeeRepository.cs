namespace Warehouse.Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Employee Add(Employee employee)
    {
        if (employee.IsTransient())
        {
            return _context.Employees
                .Add(employee)
                .Entity;
        }
        else
        {
            return employee;
        }
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var employees = await _context.Employees
            .AsNoTracking()
            .ToListAsync();

        return employees;
    }

    public async Task<Employee?> GetAsync(string employeeId)
    {
        var employee = await _context.Employees
            .AsNoTracking()
            .Where(e => e.EmployeeId == employeeId)
            .SingleOrDefaultAsync();

        return employee;
    }

    public Employee Update(Employee employee)
    {
        return _context.Employees
            .Update(employee)
            .Entity;
    }
}
