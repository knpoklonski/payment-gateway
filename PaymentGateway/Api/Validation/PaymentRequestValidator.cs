using FluentValidation;
using PaymentGateway.Api.Contracts;

namespace PaymentGateway.Api.Validation;

public class PaymentRequestValidator: AbstractValidator<PaymentRequest>
{
    public PaymentRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage($"{nameof(PaymentRequest.Id)} is required")
            .Length(1, 32);
        
        RuleFor(x => x.Source)
            .NotNull().WithMessage($"{nameof(PaymentRequest.Source)} is required");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Currency)
            .NotNull().WithMessage($"{nameof(PaymentRequest.Currency)} is required");

        RuleFor(x => x.SuccessUrl)
            .NotNull().WithMessage($"{nameof(PaymentRequest.SuccessUrl)} is required")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage($"{nameof(PaymentRequest.SuccessUrl)} must be valid");
        
        RuleFor(x => x.FailureUrl)
            .NotNull().WithMessage($"{nameof(PaymentRequest.FailureUrl)} is required")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage($"{nameof(PaymentRequest.FailureUrl)} must be valid");
    }
}