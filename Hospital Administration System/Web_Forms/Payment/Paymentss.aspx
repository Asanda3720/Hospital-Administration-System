<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Paymentss.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Payment.Paymentss" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment - MediConnect</title>
    <!-- Add Stripe.js -->
    <script src="https://js.stripe.com/v3/"></script>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    
    <style type="text/css">
        :root {
            --primary-color: #2a9d8f;
            --secondary-color: #264653;
            --accent-color: #e9c46a;
            --light-color: #f8f9fa;
            --dark-color: #212529;
            --stripe-purple: #635bff;
        }
        
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            color: var(--dark-color);
            background-color: #ffffff;
            line-height: 1.6;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
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
            padding: 1rem 0;
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
            margin: 3rem auto;
            padding: 2.5rem;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 5px 25px rgba(0, 0, 0, 0.1);
            flex-grow: 1;
        }

        .payment-title {
            font-size: 1.8rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 1.5rem;
            text-align: center;
        }

        .payment-title span {
            color: var(--primary-color);
        }

        .form-group label {
            display: block;
            font-weight: 500;
            color: var(--secondary-color);
            margin-bottom: 0.5rem;
        }

        .StripeElement {
            box-sizing: border-box;
            height: 48px;
            padding: 12px 15px;
            border: 1px solid #ddd;
            border-radius: 6px;
            background-color: white;
            margin-bottom: 20px;
            transition: all 0.3s;
        }

        .StripeElement--focus {
            border-color: var(--stripe-purple);
            box-shadow: 0 0 0 2px rgba(99, 91, 255, 0.2);
        }

        .StripeElement--invalid {
            border-color: #ff3860;
        }

        #card-errors {
            color: #ff3860;
            margin-bottom: 20px;
            font-size: 0.9rem;
        }

        .btn-pay {
            background-color: var(--stripe-purple);
            color: white;
            padding: 0.8rem 2rem;
            font-size: 1rem;
            font-weight: 500;
            border-radius: 6px;
            border: none;
            width: 100%;
            transition: all 0.3s;
        }

        .btn-pay:hover {
            background-color: #4f46e5;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(99, 91, 255, 0.3);
        }

        /* Stripe Footer */
        .stripe-footer {
            background-color: #f6f9fc;
            padding: 2.5rem 0;
            text-align: center;
            margin-top: auto;
        }

        .stripe-powered {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 10px;
            font-size: 1.2rem;
            color: #6772e5;
            font-weight: 500;
        }

        .stripe-logo {
            height: 30px;
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .payment-container {
                padding: 1.5rem;
                margin: 1.5rem;
            }
            
            .top-info-content {
                justify-content: center;
                text-align: center;
            }
            
            .contact-info a {
                display: block;
                margin: 0 0 0.5rem 0;
            }
        }
        /* Add to your existing styles */
.form-control {
    height: 48px;
    padding: 12px 15px;
    border: 1px solid #ddd;
    border-radius: 6px;
    margin-bottom: 20px;
    width: 100%;
    transition: all 0.3s;
}

.form-control:focus {
    border-color: #635bff;
    box-shadow: 0 0 0 2px rgba(99, 91, 255, 0.2);
}

#payment-message {
    padding: 12px;
    border-radius: 4px;
    margin-top: 15px;
}

.text-success {
    color: #2a9d8f;
}

.d-none {
    display: none;
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
                                <a class="nav-link active" href="/Web_Forms/Home.aspx">HOME</a>
                            </li>
                                                                                    <%
                                //HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "1")
                                    {

                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/DirectAppointmentBookings.aspx'>APPOINTMENT</a>");
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Emergency/RequestEmergency.aspx'>EMERGENCY</a>");
                                    }
                                }
                                
%>
                            <li class="nav-item">
                               <a class="nav-link" href="/Web_Forms/Doctors.aspx">DOCTORS</a>
                            </li>
                            <%--<li class="nav-item">
                                <a class="nav-link active" href="/Payment">PAYMENT</a>
                            </li>--%>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>

        <!-- Payment Form -->
       <!-- Payment Form -->
<div class="container">
    <div class="payment-container">
        <h1 class="payment-title">Secure <span>Payment</span></h1>
        
        <form id="payment-form">
            <div class="form-group">
                <label for="amount">Amount (Rands)</label>
                <input type="number" id="amount" name="amount" min="0.50" step="0.01" 
       class="form-control mb-3" value="90.00" required disabled />
            </div>
            
            <div class="form-group">
                <label for="card-element">Credit or Debit Card</label>
                <div id="card-element" class="StripeElement">
                    <!-- Stripe Element will be inserted here -->
                </div>
                <div id="card-errors" role="alert"></div>
            </div>
            
            <%--<button id="submit-button" class="btn-pay">
                <i class="fas fa-lock"></i> Pay Now
            </button>--%>
            <asp:Button ID="btnSubmit" runat="server" Text="Pay Now" CssClass="btn-pay" OnClick="btnSubmit_Click" />
            
            <div class="mt-3">
                <div id="payment-message" class="d-none"></div>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </form>
    </div>
</div>

        <!-- Stripe Footer -->
        <footer class="stripe-footer">
            <div class="container">
                <div class="stripe-powered">
                    <span>Powered by</span>
                    <img src="https://stripe.com/img/v3/home/social.png" alt="Stripe" class="stripe-logo">
                </div>
            </div>
        </footer>
    </form>

    <!-- Bootstrap JS Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

<script>
    // Initialize Stripe
    var stripe = Stripe('<%= ConfigurationManager.AppSettings["StripePublishableKey"] %>');
    var elements = stripe.elements();

    // Card element setup
    var card = elements.create('card', {
        style: {
            base: {
                fontSize: '16px',
                color: '#32325d',
                '::placeholder': {
                    color: '#aab7c4'
                }
            },
            invalid: {
                color: '#fa755a',
                iconColor: '#fa755a'
            }
        }
    });
    card.mount('#card-element');

    // Handle form submission
    document.getElementById('btnSubmit').addEventListener('click', async function (event) {
        event.preventDefault();

        // Disable submission
        var submitButton = document.getElementById('btnSubmit');
        submitButton.disabled = true;
        submitButton.value = 'Processing...';

        // Reset message
        var paymentMessage = document.getElementById('payment-message');
        paymentMessage.classList.remove('d-none', 'text-success', 'text-danger');
        paymentMessage.textContent = '';

        // Get amount
        var amount = parseFloat(document.getElementById('amount').value);
        if (isNaN(amount) || amount < 0.5) {
            showError('Amount must be at least R0.50');
            submitButton.disabled = false;
            submitButton.value = 'Pay Now';
            return;
        }

        // Verify card details with Stripe
        const { paymentMethod, error } = await stripe.createPaymentMethod({
            type: 'card',
            card: card,
            billing_details: {
                email: '<%= Request.Cookies["userInfo"]?["email"] ?? "customer@example.com" %>'
            }
        });

        if (error) {
            showError(error.message);
            submitButton.disabled = false;
            submitButton.value = 'Pay Now';
        } else {
            // If card is valid, create the appointment via AJAX
            createAppointment();
        }
    });

    function createAppointment() {
        // Get user email from cookie or page
        const userEmail = '<%= Request.Cookies["userInfo"]?["email"] ?? "" %>';

        fetch('<%= ResolveUrl("~/Web_Forms/Payment/Paymentss.aspx/CreateAppointment") %>', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            userEmail: userEmail
        }),
    })
    .then(response => response.json())
    .then(data => {
        if (data.d.Success) {
            showSuccess('Appointment booked successfully! Redirecting...');
            window.location.href = '<%= ResolveUrl("~/Web_Forms/Payment/Receipts.aspx") %>?id=' + data.d.UserId;
        } else {
            showError(data.d.Message || 'Failed to create appointment');
            document.getElementById('btnSubmit').disabled = false;
            document.getElementById('btnSubmit').value = 'Pay Now';
        }
    })
            .catch(error => {
                showError('An error occurred. Please try again.');
                document.getElementById('btnSubmit').disabled = false;
                document.getElementById('btnSubmit').value = 'Pay Now';
            });
    }
    function showError(message) {
        var paymentMessage = document.getElementById('payment-message');
        paymentMessage.textContent = message;
        paymentMessage.classList.add('text-danger');
        paymentMessage.classList.remove('d-none');
    }

    function showSuccess(message) {
        var paymentMessage = document.getElementById('payment-message');
        paymentMessage.textContent = message;
        paymentMessage.classList.add('text-success');
        paymentMessage.classList.remove('d-none');
    }

    // Show any errors in the card element
    card.addEventListener('change', function (event) {
        var displayError = document.getElementById('card-errors');
        if (event.error) {
            displayError.textContent = event.error.message;
        } else {
            displayError.textContent = '';
        }
    });
</script>

</body>
</html>