using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.Domain.SeedWork;
using Warehouse.Infrastructure.EntityConfigurations;

namespace Warehouse.Infrastructure;

public class WarehouseDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "warehouse";

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketInconsistency> BasketInconsistencies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<GoodsIssue> GoodsIssues { get; set; }
    public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<StorageSlot> StorageSlots { get; set; }
    public DbSet<StockCardEntry> StockCardEntries { get; set; }

    private IDbContextTransaction? _currentTransaction;
    private readonly IMediator _mediator;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public WarehouseDbContext(DbContextOptions options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public WarehouseDbContext(DbContextOptions options, IMediator mediator) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BasketEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BasketInconsistencyEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsIssueEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsIssueEntryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsIssueEntryBasketEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GoodsReceiptEntryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StockCardEntryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StorageSlotEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
