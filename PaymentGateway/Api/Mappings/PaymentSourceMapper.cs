using PaymentsGateway.Domain;
using PaymentsGateway.Domain.PaymentSources;

namespace PaymentGateway.Api.Mappings;

public static class PaymentSourceMapper
{
    public static CardPaymentSource ToDomainModel(this Contracts.PaymentSources.CardPaymentSource src)
    {
        return new CardPaymentSource(
            (PaymentSourceType)src.Type,
            src.Number,
            src.Holder,
            src.CVV,
            src.ExpiryMonth,
            src.ExpiryYear);
    }
    
    public static Contracts.PaymentSources.CardPaymentSource ToResponseModel(this CardPaymentSource src)
    {
        return new Contracts.PaymentSources.CardPaymentSource
        {
            Type = (Contracts.PaymentSourceType) src.Type,
            Number = src.Number,
            Holder = src.Holder,
            CVV = src.CVV,
            ExpiryMonth = src.ExpiryMonth,
            ExpiryYear = src.ExpiryYear
        };
    }
}