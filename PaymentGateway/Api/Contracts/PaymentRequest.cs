using PaymentGateway.Api.Contracts.PaymentSources;

namespace PaymentGateway.Api.Contracts;
#nullable disable
public class PaymentRequest
{
    public string Id { get; set; }
    
    public CardPaymentSource Source { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; }
    
    public string SuccessUrl { get; set; }
    
    public string FailureUrl { get; set; }
    
    public string Details { get; set; }
}