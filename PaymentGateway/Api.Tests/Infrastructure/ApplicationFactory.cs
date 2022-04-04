using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using PaymentsGateway.Domain;

namespace PaymentsGateway.Api.Tests.Infrastructure;

public sealed class ApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection> _setupAction;

    public ApplicationFactory(Action<IServiceCollection> setupAction)
    {
        _setupAction = setupAction;
        ClientOptions.AllowAutoRedirect = false;
        ClientOptions.BaseAddress = new Uri("https://paymentgateway");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
    
        builder.ConfigureTestServices(_setupAction);
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Test");
    }
}