using PaymentsGateway.Domain.PaymentSources;

namespace PaymentsGateway.Domain;

public interface IAcquiringBankClient
{
    Task<PaymentProcessResult> ProcessPayment(Payment payment, CancellationToken ct);
}

public class AcquiringBankClient : IAcquiringBankClient
{
    public Task<PaymentProcessResult> ProcessPayment(Payment payment, CancellationToken ct)
    {
        if (payment.Source.Type != PaymentSourceType.Card)
        {
            return Task.FromResult(new PaymentProcessResult {Error = "Not supported payment type"});
        }

        var source = (CardPaymentSource) payment.Source;
        var result = (source.Number, source.CVV) switch
        {
            ("4485040371536584", "100") => new PaymentProcessResult(),
            ("5588686116426417", "257") => new PaymentProcessResult(),
            ("4539628347117863", _) => new PaymentProcessResult{Error = "Not authenticated"},
            ("4275765574319271", _) => new PaymentProcessResult{Error = "Authentication rejected"},
            ("4484070000035519", "257") => new PaymentProcessResult{Error = "Card not enrolled"},
            _ => new PaymentProcessResult()
        };

        return Task.FromResult(result);
    }
}