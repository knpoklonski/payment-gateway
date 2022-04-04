namespace PaymentsGateway.Domain.PaymentSources;

public class CardPaymentSource
{
    public CardPaymentSource(PaymentSourceType type, string number, string holder, string cvv, int expiryMonth, int expiryYear)
    {
        Type = type;
        Number = number ?? throw new ArgumentNullException(nameof(number));
        Holder = holder ?? throw new ArgumentNullException(nameof(holder));
        CVV = cvv ?? throw new ArgumentNullException(nameof(cvv));
        ExpiryMonth = expiryMonth;
        ExpiryYear = expiryYear;
    }

    public PaymentSourceType Type { get;}

    public string Number { get;  }
    
    public string Holder { get; }
    
    public string CVV { get; }

    public int ExpiryMonth { get; }
    
    public int ExpiryYear { get; }
}