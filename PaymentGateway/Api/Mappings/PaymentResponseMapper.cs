using PaymentGateway.Api.Contracts;
using PaymentsGateway.Domain;

namespace PaymentGateway.Api.Mappings;

public static class PaymentResponseMapper
{
    public static PaymentResponse ToResponseDto(this Payment payment)
    {
        return new PaymentResponse
        {
            Id = payment.Id,
            Amount = payment.Amount,
            Currency = payment.Currency,
            Details = payment.Details,
            Source = payment.Source.ToResponseModel()
        };
    }
}