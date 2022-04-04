namespace PaymentGateway.Api.Contracts.PaymentSources;
#nullable disable
public class CardPaymentSource
{
    public PaymentSourceType Type { get; set; }
    
    public string Number { get; set; }

    public string Holder { get; set; }
    
    public string CVV { get; set; }

    public int ExpiryMonth { get; set; }
    
    public int ExpiryYear { get; set; }
}