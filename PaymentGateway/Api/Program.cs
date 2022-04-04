using PaymentGateway.Api;

var builder = WebApplication.CreateBuilder(args);

await WebStartup.Run(builder);

// Declare types in namespaces
#pragma warning disable CA1050
public abstract partial class Program
{
    // Expose the Program class for use with WebApplicationFactory<T>
}
#pragma warning restore CA1050