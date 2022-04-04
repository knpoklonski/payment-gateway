using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Contracts;
using PaymentGateway.Api.Mappings;
using PaymentsGateway.Domain.AddPayment;
using PaymentsGateway.Domain.GetPayment;

namespace PaymentGateway.Api.Endpoints;

public static class PaymentEndpoints
{
    public static void AddPaymentsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/v1/payments/{id}",
                async (string id, [FromServices] IMediator mediator, CancellationToken ct) =>
                {
                    var result = await mediator.Send(new GetPaymentRequest(id));
                    if (result == null)
                    {
                        return Results.NotFound();
                    }
                    
                    return Results.Ok(result.ToResponseDto());
                })
            .WithDisplayName("Get")
            .WithTags("Payments");

        app.MapPost("/api/v1/payments",
                async ([FromBody]PaymentRequest request, [FromServices] IValidator<PaymentRequest> validator, [FromServices] IMediator mediator, CancellationToken ct) =>
                {
                    var validationResult = await validator.ValidateAsync(request, ct);
                    if (!validationResult.IsValid)
                    {
                        return Results.BadRequest(validationResult.Errors);
                    }
                    
                    var addPaymentRequest = new AddPaymentRequest(request.Id,
                        request.Source.ToDomainModel(),
                        request.Amount,
                        request.Currency,
                        request.SuccessUrl,
                        request.FailureUrl,
                        request.Details
                    );
                    
                    var response = await mediator.Send(addPaymentRequest, ct);
                    
                    return response.IsSuccess 
                        ? Results.Accepted() 
                        : Results.UnprocessableEntity();
                })
            .WithDisplayName("Post")
            .WithTags("Payments");
    }
}