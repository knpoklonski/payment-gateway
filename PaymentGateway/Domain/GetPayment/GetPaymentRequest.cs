using MediatR;

namespace PaymentsGateway.Domain.GetPayment;

public class GetPaymentRequest : IRequest<Payment?>
{
    public GetPaymentRequest(string id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public string Id { get; }
}