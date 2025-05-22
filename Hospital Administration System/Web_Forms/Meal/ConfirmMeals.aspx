<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmMeals.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Meal.ConfirmMeals" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirm Meals - MediConnect</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    
    <style type="text/css">
        :root {
            --primary-color: #2a9d8f;
            --secondary-color: #264653;
            --accent-color: #e9c46a;
            --light-color: #f8f9fa;
            --dark-color: #212529;
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
        }

        /* Main Content Styles */
        .confirmation-container {
            max-width: 800px;
            margin: 2rem auto;
            padding: 2rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }

        .confirmation-title {
            font-size: 1.8rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 1.5rem;
            text-align: center;
        }

        .confirmation-title span {
            color: var(--primary-color);
        }

        .meal-card {
            background-color: #f8f9fa;
            border-radius: 8px;
            padding: 1.5rem;
            margin-bottom: 1.5rem;
            border-left: 4px solid var(--primary-color);
        }

        .meal-time {
            font-size: 1.2rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 0.5rem;
        }

        .meal-item {
            font-size: 1.1rem;
            padding: 0.75rem;
            background-color: white;
            border-radius: 4px;
            border: 1px solid #dee2e6;
            margin-top: 0.5rem;
        }

        .button-group {
            display: flex;
            justify-content: center;
            gap: 1rem;
            margin-top: 2rem;
        }

        .btn {
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            font-weight: 500;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s;
            border: none;
        }

        .btn-primary {
            background-color: var(--primary-color);
            color: white;
        }

        .btn-primary:hover {
            background-color: #21867a;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }

        .btn-secondary:hover {
            background-color: #5a6268;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        /* Footer Styles */
        .footer {
            background-color: var(--secondary-color);
            color: white;
            padding: 2rem 0;
            text-align: center;
            margin-top: 2rem;
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
            
            .confirmation-container {
                padding: 1.5rem;
                margin: 1rem;
            }
            
            .button-group {
                flex-direction: column;
                align-items: center;
            }
        }
    </style>
</head>
<body>
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
            Response.Write("<a href='/Web_Forms/Account/LogOut.aspx' class='user-email'><i class='fas fa-user-circle'></i> " + email + "</a>");
        }
        else
        {
            Response.Write("<a href='/Web_Forms/Login.aspx' class='login-link'><i class='fas fa-user'></i> Login</a>");
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
                <a class="navbar-brand" href="/Web_Forms/Home.aspx">Medi<span>Connect</span></a>
                
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
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <!-- Main Content -->
    <div class="confirmation-container">
        
        <form id="form1" runat="server">
        <h1 class="confirmation-title">Confirm Your <span>Meals</span></h1>
        
            <div class="meal-card">
                <div class="meal-time">Breakfast</div>
                <div class="meal-item">
                    <asp:Label ID="TextBox1" runat="server" Enabled="False"></asp:Label>
                </div>
            </div>
            
            <div class="meal-card">
                <div class="meal-time">Lunch</div>
                <div class="meal-item">
                    <asp:Label ID="TextBox2" runat="server" Enabled="False"></asp:Label>
                </div>
            </div>
            
            <div class="meal-card">
                <div class="meal-time">Dinner</div>
                <div class="meal-item">
                    <asp:Label ID="TextBox3" runat="server" Enabled="False"></asp:Label>
                </div>
            </div>
            
            <div class="button-group">
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClick="btnBack_Click1" CausesValidation="False" />
                <asp:Button ID="btnYes" runat="server" Text="Confirm Meals" CssClass="btn btn-primary" OnClick="btnYes_Click" />
            </div>
        </form>
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
            <div class="social-icons mb-3">
                <a href="#" class="text-white mx-2"><i class="fab fa-facebook-f"></i></a>
                <a href="#" class="text-white mx-2"><i class="fab fa-twitter"></i></a>
                <a href="#" class="text-white mx-2"><i class="fab fa-instagram"></i></a>
                <a href="#" class="text-white mx-2"><i class="fab fa-linkedin-in"></i></a>
            </div>
            <div class="copyright">
                &copy; <script>document.write(new Date().getFullYear())</script> MediConnect. All rights reserved.
            </div>
        </div>
    </footer>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>