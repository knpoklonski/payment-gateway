using MediatR;
using PaymentsGateway.Domain.PaymentSources;

namespace PaymentsGateway.Domain.AddPayment;

public class AddPaymentRequest : IRequest<AddPaymentResponse>
{
    public AddPaymentRequest(string id, CardPaymentSource source, decimal amount, string currency, string successUrl, string failureUrl, string? details)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Source = source ?? throw new ArgumentNullException(nameof(source));
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        SuccessUrl = successUrl ?? throw new ArgumentNullException(nameof(successUrl));
        FailureUrl = failureUrl ?? throw new ArgumentNullException(nameof(failureUrl));
        Details = details;
    }

    public string Id { get; }
    
    public CardPaymentSource Source { get; }
   
    public decimal Amount { get;}
    
    public string Currency { get; }

    public string SuccessUrl { get; }
    
    public string FailureUrl { get; }
    
    public string? Details { get; }
}

