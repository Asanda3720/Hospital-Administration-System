<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Payment.Payment" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secure Payment - MediConnect</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    <!-- Stripe Elements -->
    <script src="https://js.stripe.com/v3/"></script>
    
    <style type="text/css">
        :root {
            --primary-color: #2a9d8f;
            --secondary-color: #264653;
            --accent-color: #e9c46a;
            --light-color: #f8f9fa;
            --dark-color: #212529;
            --error-color: #dc3545;
        }
        
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            color: var(--dark-color);
            background-color: var(--light-color);
            line-height: 1.6;
        }
        
        /* Header Styles */
        .top-info-bar {
            background-color: var(--secondary-color);
            color: white;
            padding: 0.5rem 0;
            font-size: 0.9rem;
        }

        .top-info-content {
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            gap: 1rem;
        }

        .contact-info a {
            color: white;
            text-decoration: none;
            margin-right: 1.5rem;
        }

        .contact-info a:hover {
            color: var(--accent-color);
        }

        .main-header {
            background-color: white;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

        .navbar-brand {
            font-size: 1.8rem;
            font-weight: 700;
            color: var(--secondary-color);
        }

        .navbar-brand span {
            color: var(--primary-color);
        }

        .navbar-nav .nav-link {
            color: var(--secondary-color);
            font-weight: 500;
            padding: 0.5rem 1rem;
            margin: 0 0.25rem;
            border-radius: 4px;
        }

        .navbar-nav .nav-link:hover,
        .navbar-nav .nav-link.active {
            color: white;
            background-color: var(--primary-color);
        }

        .user-email {
            color: var(--accent-color);
            font-weight: 500;
            margin-left: 1rem;
            cursor: pointer;
        }

        .user-email:hover {
            text-decoration: underline;
        }

        .login-link {
            color: white;
            text-decoration: none;
        }

        .login-link:hover {
            color: var(--accent-color);
        }

        /* Payment Form Styles */
        .payment-container {
            max-width: 600px;
            margin: 2rem auto;
            padding: 2rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }

        .payment-title {
            font-size: 1.8rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 1.5rem;
            border-bottom: 2px solid var(--primary-color);
            padding-bottom: 0.5rem;
        }

        .payment-title span {
            color: var(--primary-color);
        }

        .amount-display {
            font-size: 1.5rem;
            font-weight: 600;
            color: var(--primary-color);
            margin: 1rem 0;
            padding: 1rem;
            background-color: #f8f9fa;
            border-radius: 4px;
            text-align: center;
        }

        /* Stripe Element styles */
        .StripeElement {
            box-sizing: border-box;
            height: 40px;
            padding: 10px 12px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            background-color: white;
            margin-bottom: 1rem;
        }

        .StripeElement--focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.2rem rgba(42, 157, 143, 0.25);
        }

        .StripeElement--invalid {
            border-color: var(--error-color);
        }

        .StripeElement--webkit-autofill {
            background-color: #fefde5 !important;
        }

        #card-errors {
            color: var(--error-color);
            margin-bottom: 1rem;
            font-size: 0.9rem;
        }

        .btn-pay {
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            font-weight: 500;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s;
            border: none;
            min-width: 180px;
            text-align: center;
            background-color: var(--primary-color);
            color: white;
            width: 100%;
        }

        .btn-pay:hover {
            background-color: #21867a;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        .btn-pay:disabled {
            background-color: #cccccc;
            cursor: not-allowed;
            transform: none;
            box-shadow: none;
        }

        /* Stripe footer logo */
        .stripe-footer {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-top: 2rem;
            color: #6772e5;
            font-size: 0.9rem;
        }

        .stripe-footer img {
            height: 26px;
            margin-left: 0.5rem;
        }

        /* Footer Styles */
        .footer {
            background-color: var(--secondary-color);
            color: white;
            padding: 2rem 0;
            text-align: center;
        }

        .footer-links {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 1.5rem;
            margin-bottom: 1.5rem;
        }

        .footer-links a {
            color: white;
            text-decoration: none;
        }

        .footer-links a:hover {
            color: var(--accent-color);
        }

        .social-icons {
            margin-bottom: 1.5rem;
        }

        .social-icons a {
            color: white;
            margin: 0 0.5rem;
            font-size: 1.2rem;
        }

        .social-icons a:hover {
            color: var(--accent-color);
        }

        .copyright {
            font-size: 0.9rem;
            opacity: 0.8;
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .top-info-content {
                justify-content: center;
                text-align: center;
            }
            
            .contact-info a {
                display: block;
                margin: 0 0 0.5rem 0;
            }
            
            .payment-container {
                padding: 1.5rem;
                margin: 1rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Top Info Bar -->
        <div class="top-info-bar">
            <div class="container">
                <div class="top-info-content">
                    <div class="contact-info">
                        <a href="tel:+1234567890"><i class="fas fa-phone-alt"></i> +1 (234) 567-890</a>
                        <a href="mailto:info@mediconnect.com"><i class="fas fa-envelope"></i> info@mediconnect.com</a>
                    </div>
                    <div>
                        <%
                            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
                            if (userCookieRetriever != null)
                            {
                                string email = userCookieRetriever["email"];
                                Response.Write("<a href='Web_Forms/LogOut.aspx' class='user-email'><i class='fas fa-user-circle'></i> " + email + "</a>");
                            }
                            else
                            {
                                Response.Write("<a href='Login.aspx' class='login-link'><i class='fas fa-user'></i> Login</a>");
                            }
                        %>
                    </div>
                </div>
            </div>
        </div>

        <!-- Main Navigation -->
        <header class="main-header">
            <nav class="navbar navbar-expand-lg navbar-light">
                <div class="container">
                    <a class="navbar-brand" href="/">Medi<span>Connect</span></a>
                    
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" 
                        aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    
                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav ms-auto">
                            <li class="nav-item">
                                <a class="nav-link" href="/">HOME</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="/Request">REQUEST</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="/Emergency">EMERGENCY</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="/Doctors">DOCTORS</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>

        <!-- Main Content -->
        <div class="payment-container">
            <h1 class="payment-title">Secure <span>Payment</span></h1>
            
            <div id="payment-form">
                <div class="form-group">
                    <label>Amount to Pay (Rands)</label>
                    <div class="amount-display">
                        <asp:TextBox ID="amount" runat="server" CssClass="form-control-plaintext text-center" Enabled="False" BorderStyle="None" Font-Bold="True" Font-Size="Medium"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group">
                    <label for="card-element">Card Details</label>
                    <div id="card-element">
                        <!-- Stripe Card Element will be inserted here -->
                    </div>
                    <div id="card-errors" role="alert"></div>
                </div>
                
                <button type="button" id="submit-button" class="btn-pay">
                    <span id="button-text">Pay Now</span>
                    <span id="button-spinner" class="spinner-border spinner-border-sm" role="status" aria-hidden="true" style="display: none;"></span>
                </button>
                
                <div class="stripe-footer">
                    <span>Powered by</span>
                    <img src="https://stripe.com/img/v3/homepage/static/logos/stripe.svg" alt="Stripe" />
                </div>
            </div>
            
            <!-- Hidden button for server-side processing -->
            <asp:Button ID="Button1" runat="server" OnClick="btnSubmit_Click" style="display: none;" />
        </div>

        <!-- Footer -->
        <footer class="footer">
            <div class="container">
                <div class="footer-links">
                    <a href="/About">About Us</a>
                    <a href="/Services">Services</a>
                    <a href="/Contact">Contact</a>
                    <a href="/Privacy">Privacy Policy</a>
                    <a href="/Terms">Terms of Service</a>
                </div>
                <div class="social-icons">
                    <a href="#"><i class="fab fa-facebook-f"></i></a>
                    <a href="#"><i class="fab fa-twitter"></i></a>
                    <a href="#"><i class="fab fa-instagram"></i></a>
                    <a href="#"><i class="fab fa-linkedin-in"></i></a>
                </div>
                <div class="copyright">
                    &copy; <script>document.write(new Date().getFullYear())</script> MediConnect. All rights reserved.
                </div>
            </div>
        </footer>
    </form>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    
    <!-- Stripe Integration Script -->
    <script type="text/javascript">
        // This is your publishable API key from web.config
        var publishableKey = '<%= ConfigurationManager.AppSettings["StripePublishableKey"] %>';
        var stripe = Stripe(publishableKey);
        var elements = stripe.elements();
        
        // Custom styling for the Stripe Elements
        var style = {
            base: {
                color: '#32325d',
                fontFamily: '"Poppins", sans-serif',
                fontSmoothing: 'antialiased',
                fontSize: '16px',
                '::placeholder': {
                    color: '#aab7c4'
                }
            },
            invalid: {
                color: '#dc3545',
                iconColor: '#dc3545'
            }
        };
        
        // Create an instance of the card Element
        var card = elements.create('card', { style: style });
        
        // Add an instance of the card Element into the `card-element` div
        card.mount('#card-element');
        
        // Handle real-time validation errors from the card Element
        card.addEventListener('change', function(event) {
            var displayError = document.getElementById('card-errors');
            if (event.error) {
                displayError.textContent = event.error.message;
            } else {
                displayError.textContent = '';
            }
        });
        
        // Handle form submission
        var form = document.getElementById('payment-form');
        form.addEventListener('submit', function(event) {
            event.preventDefault();
            
            // Disable the submit button to prevent repeated clicks
            var submitButton = document.getElementById('submit-button');
            submitButton.disabled = true;
            document.getElementById('button-text').style.display = 'none';
            document.getElementById('button-spinner').style.display = 'inline-block';
            
            // Get the amount from the server control
            var amount = document.getElementById('<%= amount.ClientID %>').value;
            
            // Create payment intent on server and then confirm card payment
            fetch('/Web_Forms/Payment/Payment.aspx/CreatePaymentIntent', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ amount: amount })
            })
            .then(function(response) {
                return response.json();
            })
            .then(function(data) {
                var clientSecret = data.d.clientSecret;
                
                return stripe.confirmCardPayment(clientSecret, {
                    payment_method: {
                        card: card,
                        billing_details: {
                            email: '<%= Request.Cookies["userInfo"]?["email"] %>'
                        }
                    }
                });
            })
            .then(function(result) {
                if (result.error) {
                    // Show error to your customer
                    var errorElement = document.getElementById('card-errors');
                    errorElement.textContent = result.error.message;
                    
                    // Re-enable the submit button
                    submitButton.disabled = false;
                    document.getElementById('button-text').style.display = 'inline-block';
                    document.getElementById('button-spinner').style.display = 'none';
                } else {
                    if (result.paymentIntent.status === 'succeeded') {
                        // The payment is complete!
                        // Submit the form to your server to complete the transaction
                        document.getElementById('<%= btnSubmit.ClientID %>').click();
                    }
                }
            })
            .catch(function(error) {
                console.error('Error:', error);
                // Re-enable the submit button
                submitButton.disabled = false;
                document.getElementById('button-text').style.display = 'inline-block';
                document.getElementById('button-spinner').style.display = 'none';
            });
        });
    </script>
    
    <!-- Hidden button for server-side processing -->
    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" style="display: none;" />
</body>
</html>