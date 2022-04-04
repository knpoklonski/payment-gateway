using System.Text.Json.Serialization;
using PaymentsGateway.Domain.PaymentSources;

namespace PaymentsGateway.Domain;

public class Payment
{
    
    [JsonConstructor]
    public Payment(string id, CardPaymentSource source, decimal amount, string currency, string successUrl, string failureUrl, string? details)
    {
        Id = id;
        Source = source;
        Amount = amount;
        Currency = currency;
        SuccessUrl = successUrl;
        FailureUrl = failureUrl;
        Details = details;
    }

    public string Id { get; }

    public CardPaymentSource Source { get; }

    public decimal Amount { get; }

    public string Currency { get; }

    public string SuccessUrl { get; }

    public string FailureUrl { get; }

    public string? Details { get; }
}