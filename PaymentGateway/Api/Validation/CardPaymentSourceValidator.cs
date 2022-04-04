using FluentValidation;
using PaymentsGateway.Domain;
using PaymentsGateway.Domain.PaymentSources;

namespace PaymentGateway.Api.Validation;

public class CardPaymentSourceValidator : AbstractValidator<CardPaymentSource>
{
    public CardPaymentSourceValidator()
    {
        RuleFor(x => x.Type)
            .Equal(PaymentSourceType.Card);

        RuleFor(x => x.Number)
            .NotNull().WithMessage($"{nameof(CardPaymentSource.Number)} is required")
            .Length(12, 19);
        
        RuleFor(x => x.Holder)
            .NotNull().WithMessage($"{nameof(CardPaymentSource.Holder)} is required")
            .Length(2, 26)
            .Matches("(?<! )[-a-zA-Z' ]{2,26}");
        
        RuleFor(x => x.CVV)
            .NotNull().WithMessage($"{nameof(CardPaymentSource.CVV)} is required")
            .Length(2, 4)
            .Matches("^[0-9]+$");
        
        RuleFor(x => x.ExpiryMonth)
            .NotNull().WithMessage($"{nameof(CardPaymentSource.ExpiryMonth)} is required")
            .InclusiveBetween(1, 12);
        
        RuleFor(x => x.ExpiryYear)
            .NotNull().WithMessage($"{nameof(CardPaymentSource.ExpiryYear)} is required")
            .InclusiveBetween(2021, 3000);
    }
}