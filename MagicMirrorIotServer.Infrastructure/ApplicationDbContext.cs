using MagicMirrorIotServer.Domain.AggregateModels.Metrics;
using MagicMirrorIotServer.Domain.SeedWork;
using MagicMirrorIotServer.Infrastructure.EntityConfigurations;
using MagicMirrorIotServer.Infrastructure.EntityConfigurations.TagReadings;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace MagicMirrorIotServer.Infrastructure;
public class ApplicationDbContext: DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "iotserver";

    private IDbContextTransaction? _currentTransaction;
    private readonly IMediator _mediator;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationDbContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public DbSet<EonNode> EonNodes { get; set; }
    public DbSet<TagReading<int>> IntTagReadings { get; set; }
    public DbSet<TagReading<double>> DoubleTagReadings { get; set; }
    public DbSet<TagReading<bool>> BoolTagReadings { get; set; }
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeviceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EonNodeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new BoolTagReadingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DoubleTagReadingEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new IntTagReadingEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync();

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
