namespace PaymentsGateway.Domain;

public class PaymentProcessResult
{
    public bool Success => string.IsNullOrEmpty(Error);
    public string? Error { get; set; }
}