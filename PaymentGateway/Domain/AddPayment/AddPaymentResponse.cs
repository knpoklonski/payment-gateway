namespace PaymentsGateway.Domain.AddPayment;

public class AddPaymentResponse
{
    public bool IsSuccess { get; }
    public string RedirectUrl { get;}

    private AddPaymentResponse(bool success, string redirectUrl)
    {
        IsSuccess = success;
        RedirectUrl = redirectUrl;
    }

    public static AddPaymentResponse Success(string url)
    {
        return new AddPaymentResponse(true, url);
    }
    
    public static AddPaymentResponse Failure(string url)
    {
        return new AddPaymentResponse(false, url);
    }
}