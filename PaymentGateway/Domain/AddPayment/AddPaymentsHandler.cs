using MediatR;
using PaymentsGateway.Domain.PaymentSources;

namespace PaymentsGateway.Domain.AddPayment;

public class AddPaymentsHandler : IRequestHandler<AddPaymentRequest, AddPaymentResponse>
{
    private readonly IPaymentsRepository _repository;
    private readonly IAcquiringBankClient _bankClient;

    public AddPaymentsHandler(IPaymentsRepository repository, IAcquiringBankClient bankClient)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _bankClient = bankClient ?? throw new ArgumentNullException(nameof(bankClient));
    }

    public async Task<AddPaymentResponse> Handle(AddPaymentRequest request, CancellationToken ct)
    {
        var payment = new Payment(request.Id,
            request.Source as CardPaymentSource,
            request.Amount,
            request.Currency,
            request.SuccessUrl,
            request.FailureUrl,
            request.Details);

        await _repository.Save(payment, ct);

        var processResult = await _bankClient.ProcessPayment(payment, ct);

        return processResult.Success
            ? AddPaymentResponse.Success(payment.SuccessUrl)
            : AddPaymentResponse.Failure(payment.FailureUrl);
    }
}