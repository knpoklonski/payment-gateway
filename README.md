# payment-gateway
* Run solution
* go to swagger/index.html
* There are two enpoints GET, POST, try use payload below
 ```json
{
  "id": "123pay2529",
  "source": {
    "type": 1,
    "number": "4485040371536584",
    "holder": "Konstantin P",
    "cvv": "123",
    "expiryMonth": 12,
    "expiryYear": 2026
  },
  "amount": 100,
  "currency": "EUR",
  "successUrl": "https://checkout.com",
  "failureUrl": "https://stripe.com",
  "details": "some order details"
}
 ```
 * solution uses SQLite database, payments.db within repository for simplicity
