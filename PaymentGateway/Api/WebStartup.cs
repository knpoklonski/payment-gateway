using CorrelationId;
using CorrelationId.DependencyInjection;
using DataAccess;
using FluentMigrator.Runner;
using FluentValidation;
using MediatR;
using PaymentGateway.Api.Contracts;
using PaymentGateway.Api.Endpoints;
using PaymentGateway.Api.Infrastructure;
using PaymentGateway.Api.Infrastructure.Migrations;
using PaymentGateway.Api.Validation;
using PaymentsGateway.Domain;
using PaymentsGateway.Domain.AddPayment;
using PaymentsGateway.Domain.PaymentSources;
using PaymentsGateway.Domain.Settings;

namespace PaymentGateway.Api;

public static class WebStartup
{
    public static async Task Run(WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.Services.Configure<DbOptions>(builder.Configuration.GetSection(nameof(DbOptions)));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDefaultCorrelationId();
        builder.Services.AddMigrations(builder.Configuration.GetValue<string>($"{nameof(DbOptions)}:{nameof(DbOptions.ConnectionString)}"));
        builder.Services.AddServices();
        builder.Services.AddHostedService<MigrationsBgRunner>();

        var app = builder.Build();
        app.UseCorrelationId();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.AddPaymentsEndpoints();
        
        app.UseRouting();
        
        await app.RunAsync();
    }

    private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<CardPaymentSource>, CardPaymentSourceValidator>();
        serviceCollection.AddScoped<IValidator<PaymentRequest>, PaymentRequestValidator>();
        
        serviceCollection.AddTransient<IPaymentsRepository, PaymentsRepository>();
        serviceCollection.AddTransient<IAcquiringBankClient, AcquiringBankClient>();
        serviceCollection.AddMediatR(typeof(AddPaymentsHandler).Assembly);
        
        return serviceCollection;
    }
    
    private static IServiceCollection AddMigrations(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddFluentMigratorCore()
            .ConfigureRunner(builder => builder
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Migration202202042132).Assembly).For.Migrations());
        
        return serviceCollection;
    }
}