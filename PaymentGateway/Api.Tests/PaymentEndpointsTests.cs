using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PaymentGateway.Api.Contracts;
using PaymentsGateway.Api.Tests.Infrastructure;
using PaymentsGateway.Domain;
using PaymentsGateway.Domain.PaymentSources;
using PaymentSourceType = PaymentsGateway.Domain.PaymentSourceType;

namespace PaymentsGateway.Api.Tests;

[TestFixture]
public class PaymentEndpointsTests : ApiTestsBase
{
    [Test]
    public async Task ShouldGetPayment()
    {
        //arrange
        var payment = CreatePayment();
        PaymentsRepository
            .Setup(x =>
                x.Get(It.Is<string>("123", StringComparer.OrdinalIgnoreCase), It.IsAny<CancellationToken>()))
            .ReturnsAsync(payment);
        
        //act
        var resultStr = await ApiClient.GetStringAsync(new Uri($"/api/v1/payments/123"));
        var result = JsonSerializer.Deserialize<PaymentResponse>(resultStr, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        
        //assert
        Assert.AreEqual(new {payment.Id, payment.Amount, payment.Currency, payment.Source.Number, payment.Source.Holder, Type = (int) payment.Source.Type, payment.Source.CVV, payment.Source.ExpiryMonth, payment.Source.ExpiryYear},
            new {result.Id, result.Amount, result.Currency, result.Source.Number, result.Source.Holder,Type = (int) result.Source.Type, result.Source.CVV, result.Source.ExpiryMonth, result.Source.ExpiryYear});
    }
    
    [Test]
    public async Task ShouldGet404Error_GetPayment()
    {
        //arrange
        PaymentsRepository
            .Setup(x =>
                x.Get(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Payment?) null);
        
        //act
        var response = await ApiClient.GetAsync(new Uri($"/api/v1/payments/123"));

        //assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Test]
    public async Task ShouldPostPayment()
    {
        //arrange
        var paymentRequest = CreatePaymentRequest();
        
        //act
        var response = await ApiClient.PostAsJsonAsync(new Uri($"/api/v1/payments"), paymentRequest, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        
        //assert
        Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
    }
    
    [Test]
    public async Task ShouldReturnBadRequest_DueValidation_PostPayment()
    {
        //arrange
        var paymentRequest = CreatePaymentRequest();
        paymentRequest.Amount = -5;
        
        //act
        var response = await ApiClient.PostAsJsonAsync(new Uri($"/api/v1/payments"), paymentRequest, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        
        //assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    //ToDo add uniq test for validation
    
    private Payment CreatePayment()
    {
        return new Payment("123", 
            new CardPaymentSource(PaymentSourceType.Card, "4485040371536584", "holder", "123", 10, 2025),
            100,
            "EUR",
            "http://test.test",
            "http://test2.test",
            "details");
    }
    
    private PaymentRequest CreatePaymentRequest()
    {
        return new PaymentRequest
        {
            Id = "123",
            Amount = 100,
            Currency = "EUR",
            Details = "details",
            SuccessUrl = "http://test.test",
            FailureUrl = "http://test2.test",
            Source = new PaymentGateway.Api.Contracts.PaymentSources.CardPaymentSource()
            {
                Holder = "holder",
                Type = PaymentGateway.Api.Contracts.PaymentSourceType.Card,
                Number = "4485040371536584",
                CVV = "123",
                ExpiryMonth = 12,
                ExpiryYear = 2025
            }
        };
    }
}