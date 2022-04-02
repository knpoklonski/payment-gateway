using PaymentGateway.Api;

var builder = WebApplication.CreateBuilder(args);

await WebStartup.Run(builder);