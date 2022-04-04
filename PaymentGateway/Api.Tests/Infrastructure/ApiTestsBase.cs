using System.Net.Http;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using PaymentsGateway.Domain;

namespace PaymentsGateway.Api.Tests.Infrastructure;

public class ApiTestsBase
{
    private ApplicationFactory? _factory;
    protected HttpClient ApiClient { get; set; } = null!;
    protected Mock<IPaymentsRepository> PaymentsRepository { get; set; } = null!;

    [SetUp]
    public void Setup()
    {
        PaymentsRepository = new Mock<IPaymentsRepository>();
        _factory = new ApplicationFactory(services =>
        {
            services.AddTransient(_=> PaymentsRepository.Object);
            services.AddTransient(_=> new Mock<IMigrationRunner>().Object);
        });
        ApiClient = _factory.CreateClient();
    }

    [TearDown]
    public async Task TearDown()
    {
        ApiClient.Dispose();
        await _factory!.DisposeAsync();
    }
}