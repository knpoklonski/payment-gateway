using FluentMigrator.Runner;

namespace PaymentGateway.Api.Infrastructure;

public class MigrationsBgRunner : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MigrationsBgRunner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var migrationRunner = scope.ServiceProvider.GetService<IMigrationRunner>();
        migrationRunner?.MigrateUp();
        return Task.CompletedTask;
    }
}