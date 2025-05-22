using Stripe;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace Hospital_Administration_System.Models
{
    public class PaymentService
    {
        private readonly string _stripeSecretKey;

        public PaymentService()
        {
            _stripeSecretKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            StripeConfiguration.ApiKey = _stripeSecretKey;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(string paymentMethodId, decimal amount, string description)
        {
            try
            {
                // Validate amount
                if (amount < 0.5m) // Minimum $0.50
                {
                    return new PaymentResult
                    {
                        Success = false,
                        Message = "Amount must be at least $0.50"
                    };
                }

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100), // Convert to cents
                    Currency = "usd",
                    Description = description,
                    PaymentMethod = paymentMethodId,
                    Confirm = true,
                    ReceiptEmail = GetUserEmail(), // Implement this method
                    Metadata = new Dictionary<string, string>
                    {
                        { "user_id", GetUserId() } // Implement this method
                    }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                if (paymentIntent.Status == "succeeded")
                {
                    Trace.TraceInformation($"Payment succeeded: {paymentIntent.Id}");
                    return new PaymentResult
                    {
                        Success = true,
                        TransactionId = paymentIntent.Id,
                        Message = "Payment processed successfully"
                    };
                }

                Trace.TraceWarning($"Payment failed: {paymentIntent.LastPaymentError?.Message}");
                return new PaymentResult
                {
                    Success = false,
                    Message = paymentIntent.LastPaymentError?.Message ?? "Payment failed"
                };
            }
            catch (StripeException ex)
            {
                Trace.TraceError($"Stripe error: {ex.Message}");
                return new PaymentResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        private string GetUserEmail()
        {
            // Implement based on your authentication system
            var userCookie = HttpContext.Current.Request.Cookies["userInfo"];
            return userCookie?["email"] ?? "customer@example.com";
        }

        private string GetUserId()
        {
            // Implement based on your authentication system
            return HttpContext.Current.User.Identity.Name ?? "anonymous";
        }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
    }
}