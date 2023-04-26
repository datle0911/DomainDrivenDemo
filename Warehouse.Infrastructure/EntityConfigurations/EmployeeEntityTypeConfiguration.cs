namespace Warehouse.Infrastructure.EntityConfigurations;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> employeeConfiguration)
    {
        employeeConfiguration
            .HasKey(e => e.Id);
        employeeConfiguration
            .Property(e => e.Id)
            .UseHiLo("employeeeq", WarehouseDbContext.DEFAULT_SCHEMA);

        employeeConfiguration
            .Ignore(e => e.DomainEvents);

        employeeConfiguration
            .HasIndex(e => e.EmployeeId)
            .IsUnique();

        employeeConfiguration
            .Property(e => e.EmployeeId)
            .HasMaxLength(40)
            .IsRequired();
        employeeConfiguration
            .HasIndex(p => p.EmployeeId)
            .IsUnique();

        employeeConfiguration
            .Property(p => p.FirstName)
            .HasMaxLength(30)
            .IsRequired();
        employeeConfiguration
            .Property(p => p.LastName)
            .HasMaxLength(30)
            .IsRequired();

        employeeConfiguration
            .Property(p => p.DateOfBirth)
            .IsRequired();
    }
}
