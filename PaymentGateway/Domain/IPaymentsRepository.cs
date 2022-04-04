namespace PaymentsGateway.Domain;

public interface IPaymentsRepository
{
    Task<Payment?> Get(string paymentId, CancellationToken ct);

    Task Save(Payment payment, CancellationToken ct);
}