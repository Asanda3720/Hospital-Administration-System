<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MEDICONNECT</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    
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
            background-color: #ffffff;
            line-height: 1.6;
        }
        
        /* Top Info Bar */
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

        .search-box {
            display: flex;
            align-items: center;
            gap: 0.5rem;
        }

        .search-box input {
            border: none;
            border-radius: 4px 0 0 4px;
            padding: 0.25rem 0.5rem;
            font-size: 0.9rem;
        }

        .search-box button {
            background-color: var(--primary-color);
            border: none;
            color: white;
            padding: 0.25rem 0.75rem;
            border-radius: 0 4px 4px 0;
            cursor: pointer;
        }

        .login-link {
            color: white;
            font-weight: 500;
            text-decoration: none;
        }

        .login-link:hover {
            color: var(--accent-color);
        }

        /* Main Navigation */
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

        .navbar-toggler {
            border: none;
            padding: 0.5rem;
        }

        .navbar-toggler:focus {
            box-shadow: none;
        }

        .navbar-toggler-icon {
            background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 30 30'%3e%3cpath stroke='rgba%280, 0, 0, 0.55%29' stroke-linecap='round' stroke-miterlimit='10' stroke-width='2' d='M4 7h22M4 15h22M4 23h22'/%3e%3c/svg%3e");
        }

        /* Hero Section */
        .hero-section {
            background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
            padding: 4rem 2rem;
        }

        .hero-content-wrapper {
            display: flex;
            align-items: flex-start;
            gap: 3rem;
            max-width: 1200px;
            margin: 0 auto;
        }

        .hero-main-image {
            width: 350px;
            height: 400px;
            object-fit: cover;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            flex-shrink: 0;
        }

        .hero-text-content {
            flex: 1;
        }

        .app-name {
            font-size: 3.5rem;
            font-weight: 700;
            color: var(--secondary-color);
            margin-bottom: 1rem;
        }
        
        .app-name span {
            color: var(--primary-color);
        }
        
        .hero-subtitle {
            font-size: 1.2rem;
            color: #6c757d;
            margin-bottom: 2rem;
        }
        
        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
            padding: 0.75rem 2rem;
            font-weight: 500;
            border-radius: 50px;
            margin-right: 1rem;
            margin-bottom: 1rem;
        }
        
        .btn-primary:hover {
            background-color: #21867a;
            border-color: #21867a;
        }
        
        .login-url {
            background-color: var(--light-color);
            padding: 1rem;
            border-radius: 8px;
            margin: 1rem 0;
            max-width: 500px;
            font-family: monospace;
            word-break: break-all;
        }
        
        .social-icons {
            margin-top: 2rem;
        }
        
        .social-icons a {
            color: var(--secondary-color);
            font-size: 1.5rem;
            margin: 0 0.5rem;
            transition: color 0.3s;
        }
        
        .social-icons a:hover {
            color: var(--primary-color);
        }

        /* Services Section */
        .services-section {
            padding: 4rem 2rem;
            background-color: #ffffff;
        }
        
        .section-title {
            font-size: 2rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 1.5rem;
            text-align: center;
        }
        
        .section-title span {
            color: var(--primary-color);
        }
        
        .section-subtitle {
            color: #6c757d;
            margin-bottom: 2rem;
            text-align: center;
        }
        
        /* Footer */
        .footer {
            background-color: var(--secondary-color);
            color: white;
            padding: 2rem;
            text-align: center;
            margin-top: 3rem;
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

        /* Emergency Alert Button Styles */
        .emergency-alert-btn {
            position: fixed;
            top: 100px;
            right: 20px;
            z-index: 1000;
            background-color: #ff0000;
            color: white;
            border: none;
            padding: 15px 25px;
            border-radius: 5px;
            font-weight: bold;
            box-shadow: 0 0 20px rgba(255, 0, 0, 0.7);
            animation: pulse 1.5s infinite;
            display: none;
        }

        @keyframes pulse {
            0% {
                transform: scale(1);
                box-shadow: 0 0 0 0 rgba(255, 0, 0, 0.7);
            }
            70% {
                transform: scale(1.05);
                box-shadow: 0 0 0 15px rgba(255, 0, 0, 0);
            }
            100% {
                transform: scale(1);
                box-shadow: 0 0 0 0 rgba(255, 0, 0, 0);
            }
        }

        /* Audio element styles */
        #emergencySound {
            display: none;
        }

        /* Responsive adjustments */
        @media (max-width: 992px) {
            .hero-content-wrapper {
                flex-direction: column;
                align-items: center;
            }
            
            .hero-main-image {
                width: 100%;
                max-width: 400px;
                height: auto;
                aspect-ratio: 4/5;
                margin-bottom: 2rem;
            }
            
            .hero-text-content {
                text-align: center;
            }
            
            .btn-primary {
                margin-right: 0.5rem;
                margin-left: 0.5rem;
            }

            .top-info-content {
                justify-content: center;
            }

            .contact-info {
                text-align: center;
                margin-bottom: 0.5rem;
            }

            .search-box {
                margin-bottom: 0.5rem;
            }
        }

        @media (max-width: 768px) {
            .app-name {
                font-size: 2.8rem;
            }
            
            .hero-main-image {
                max-width: 350px;
            }
        }

        @media (max-width: 576px) {
            .chat-float-btn {
                right: 10px;
                bottom: 10px;
                padding: 12px 16px;
                font-size: 1rem;
            }

            .navbar-brand {
                font-size: 1.5rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Emergency Alert Button -->
        <button id="emergencyAlertBtn" runat="server" type="button" class="emergency-alert-btn" onclick="playEmergencySound()">
            <i class="fas fa-exclamation-triangle"></i> EMERGENCY ALERT
        </button>
        
        <!-- Audio element for emergency sound -->
        <audio id="emergencySound" loop>
            <source src="Contents/police.wav" type="audio/wav">
            Your browser does not support the audio element.
        </audio>

        <!-- Top Info Bar -->
        <div class="top-info-bar">
            <div class="container">
                <div class="top-info-content">
                    <div class="contact-info">
                        <a href="tel:0315021719"><i class="fas fa-phone-alt"></i> (031) 502 1719</a>
                        <a href="mailto:info@atma.gov.gh"><i class="fas fa-envelope"></i> info@atma.gov.gh</a>
                    </div>
                    
                   <%-- <div class="search-box">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Search content here..." CssClass="form-control-sm"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm" Text="Search" OnClick="btnSearch_Click" />
                    </div>--%>
                    
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
                    
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    
                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav ms-auto">
                            <li class="nav-item">
                                <a class="nav-link active" href="/Web_Forms/Home.aspx">HOME</a>
                            </li>
                            <%
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "1")
                                    {
                                        Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/DirectAppointmentBookings.aspx'>APPOINTMENT</a>");
                                        Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Emergency/RequestEmergency.aspx'>EMERGENCY</a>");
                                        Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Payment/Billings.aspx'>OUTSTANDING PAYMENTS</a>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Emergency/RequestEmergency.aspx'>EMERGENCY</a>");
                                }
                            %>
                            <li class="nav-item">
                                <a class="nav-link" href="/Web_Forms/Doctors.aspx">DOCTORS</a>
                            </li>
                            <%
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "0")
                                    {
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Analysis/Administration.aspx'>ADMIN</a></li>");
                                        Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Analysis/AddUsers.aspx'>ADD USERS</a></li>");
                                    }
                                }
                            %>
                            <%
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "3")
                                    {
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Lab/LabTech.aspx'>Lab Tests</a></li>");
                                    }
                                }
                            %>                            
                            <%
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "4")
                                    {
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/XRAY/AllXRAYS.aspx'>Booked X-rays</a></li>");
                                    }
                                }
                            %>
                            <%
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "5")
                                    {
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Emergency/AllEmergencies.aspx'> ALL EMERGENCIES</a></li>");
                                    }
                                }
                            %>
                            <%
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "2")
                                    {
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/AdmitPatient/AllAdmittedPatients.aspx'> ADMITTED PATIENTS</a></li>");
                                    }
                                }
                            %>
                            <%
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "6")
                                    {
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Pharmacist/PrescribedMed.aspx'> PRESCRIBED MED</a></li>");
                                    }
                                }
                            %>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        
        <!-- Hero Section -->
        <section class="hero-section">
            <div class="container">
                <div class="hero-content-wrapper">
                    <!-- Left Image -->
                    <img src="/Contents/depositphotos_2552495-stock-photo-successful-doctor-with-stethoscope.jpg" 
                         class="hero-main-image" alt="Hospital Icon" />
                    
                    <div class="hero-text-content">
                        <h1 class="app-name">MEDI<span>CONNECT</span></h1>
                        <p class="hero-subtitle">Connecting people from the outside world.</p>
                        
                        <!-- Register Button -->
                        <asp:Button ID="btnRegister" runat="server" Text="Register" 
                            CssClass="btn btn-primary" OnClick="btnRegister_Click" />

                        <!-- Book Appointment Button -->
                        <asp:Button ID="btnBookAppointment" runat="server" Text="Book Appointment" 
                            CssClass="btn btn-primary" OnClick="btnBookAppointment_Click" Visible="False" />
                        
                        <div class="login-url mt-3">
                            <strong>We are proud of our</strong> strong quality service.
                        </div>
                        
                        <div class="social-icons">
                            <p>Follow <strong>MediConnect</strong></p>
                            <a href="#"><i class="fab fa-facebook"></i></a>
                            <a href="#"><i class="fab fa-twitter"></i></a>
                            <a href="#"><i class="fab fa-instagram"></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        
        <!-- Services Section -->
        <section class="services-section">
            <div class="container">
                <h2 class="section-title">OUR <span>SERVICES</span></h2>
                <p class="section-subtitle">See the list of our services.</p>
                
                <!-- Services content would go here -->
            </div>
        </section>
        
        <!-- Footer -->
        <footer class="footer">
            <div class="container">
                <div class="footer-links">
                    <a href="#">MedConnect</a>
                    <a href="#">Propriant Lung</a>
                    <a href="#">About Licences</a>
                    <a href="#">Correos</a>
                    <a href="#">Commands & Forum</a>
                    <a href="#">Principal Policy</a>
                </div>
                <p class="copyright">MedConnect © 2025</p>
            </div>
        </footer>
    </form>
    <!-- Bootstrap JS Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <script>
        // Show emergency button for userType 5
        window.onload = function () {
            <% 
                HttpCookie userCookie = Request.Cookies["userInfo"];
                if (userCookie != null && userCookie["userType"] == "5") 
                { 
            %>
                document.getElementById('emergencyAlertBtn').style.display = 'block';
            <% } %>
        };

        function playEmergencySound() {
            var sound = document.getElementById('emergencySound');
            sound.play();
        }

        function stopEmergencySound() {
            var sound = document.getElementById('emergencySound');
            sound.pause();
            sound.currentTime = 0;
            document.getElementById('emergencyAlertBtn').style.display = 'none';
        }
    </script>
</body>
</html>