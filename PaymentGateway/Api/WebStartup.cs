using CorrelationId;
using CorrelationId.DependencyInjection;

namespace PaymentGateway.Api;

public static class WebStartup
{
    public static async Task Run(WebApplicationBuilder builder)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDefaultCorrelationId();
        builder.Services.AddRepositories();

        var app = builder.Build();
        app.UseCorrelationId();

        app.UseSwagger();
        app.UseSwaggerUI();

        await app.RunAsync();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}