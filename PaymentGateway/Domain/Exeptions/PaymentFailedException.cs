using System.Runtime.Serialization;

namespace PaymentsGateway.Domain.Exeptions;

[Serializable]
public class PaymentFailedException : Exception
{
    public PaymentFailedException()
    {
    }

    public PaymentFailedException(string message) : base(message)
    {
    }

    public PaymentFailedException(string message, Exception inner) : base(message, inner)
    {
    }

    protected PaymentFailedException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}