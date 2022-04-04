using MediatR;

namespace PaymentsGateway.Domain.GetPayment;

public class GetPaymentHandler : IRequestHandler<GetPaymentRequest, Payment?>
{
    private readonly IPaymentsRepository _paymentsRepository;

    public GetPaymentHandler(IPaymentsRepository paymentsRepository)
    {
        _paymentsRepository = paymentsRepository ?? throw new ArgumentNullException(nameof(paymentsRepository));
    }

    public Task<Payment?> Handle(GetPaymentRequest request, CancellationToken ct)
    {
        return _paymentsRepository.Get(request.Id, ct);
    }
}