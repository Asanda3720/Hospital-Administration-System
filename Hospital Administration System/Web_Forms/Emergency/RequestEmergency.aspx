<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestEmergency.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Emergency.RequestEmergency" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emergency Request - MediConnect</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&family=Roboto:wght@400;500;700&display=swap" rel="stylesheet">
    
    <style type="text/css">
        :root {
            --emergency-red: #d90429;
            --emergency-dark: #2b2d42;
            --emergency-light: #edf2f4;
            --emergency-accent: #ef233c;
            --warning-yellow: #ffd166;
        }
        
        body {
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
            color: var(--emergency-dark);
            background-color: var(--emergency-light);
            line-height: 1.6;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
        }
        
        /* Emergency Alert Bar */
        .emergency-alert-bar {
            background-color: var(--emergency-red);
            color: white;
            padding: 0.5rem 0;
            text-align: center;
            font-weight: 700;
            font-size: 1.1rem;
            animation: pulse 2s infinite;
        }

        @keyframes pulse {
            0% { background-color: var(--emergency-red); }
            50% { background-color: #ef233c; }
            100% { background-color: var(--emergency-red); }
        }

        /* Header Styles */
        .top-info-bar {
            background-color: var(--emergency-dark);
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
            color: var(--warning-yellow);
        }

        .main-header {
            background-color: white;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            border-bottom: 3px solid var(--emergency-red);
        }

        .navbar-brand {
            font-size: 1.8rem;
            font-weight: 700;
            color: var(--emergency-dark);
        }

        .navbar-brand span {
            color: var(--emergency-red);
        }

        .navbar-nav .nav-link {
            color: var(--emergency-dark);
            font-weight: 500;
            padding: 0.5rem 1rem;
            margin: 0 0.25rem;
            border-radius: 4px;
        }

        .navbar-nav .nav-link:hover,
        .navbar-nav .nav-link.active {
            color: white;
            background-color: var(--emergency-red);
        }

        .user-email {
            color: var(--warning-yellow);
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
            color: var(--warning-yellow);
        }

        /* Main Content Styles */
        .main-content {
            flex: 1;
            padding: 2rem 0;
            background-color: #fff;
            background-image: radial-gradient(circle at 10% 20%, rgba(239, 35, 60, 0.05) 0%, rgba(239, 35, 60, 0.05) 90%);
        }

        .emergency-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 2rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            border-left: 6px solid var(--emergency-red);
        }

        .emergency-title {
            font-size: 2rem;
            font-weight: 700;
            color: var(--emergency-red);
            margin-bottom: 1.5rem;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .emergency-subtitle {
            color: var(--emergency-dark);
            font-weight: 500;
            margin-bottom: 2rem;
            border-bottom: 2px solid var(--emergency-red);
            padding-bottom: 0.5rem;
        }

        .form-group {
            margin-bottom: 1.5rem;
        }

        .form-group label {
            display: block;
            font-weight: 500;
            margin-bottom: 0.5rem;
            color: var(--emergency-dark);
        }

        .form-group label.required:after {
            content: " *";
            color: var(--emergency-red);
        }

        .form-control {
            width: 100%;
            padding: 0.75rem 1rem;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 1rem;
            transition: all 0.3s;
        }

        .form-control:focus {
            border-color: var(--emergency-red);
            box-shadow: 0 0 0 0.2rem rgba(217, 4, 41, 0.25);
            outline: none;
        }

        .text-area {
            min-height: 120px;
            resize: vertical;
        }

        .btn-emergency {
            padding: 1rem 2rem;
            font-size: 1.1rem;
            font-weight: 700;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s;
            border: none;
            width: 100%;
            text-align: center;
            background-color: var(--emergency-red);
            color: white;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-top: 1rem;
        }

        .btn-emergency:hover {
            background-color: #c00424;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(217, 4, 41, 0.3);
        }

        .btn-emergency i {
            margin-right: 0.5rem;
            animation: shake 0.5s infinite alternate;
        }

        @keyframes shake {
            from { transform: rotate(-10deg); }
            to { transform: rotate(10deg); }
        }

        .assurance-message {
            text-align: center;
            font-size: 1.2rem;
            font-weight: 500;
            color: var(--emergency-red);
            margin-top: 2rem;
            padding: 1rem;
            background-color: rgba(239, 35, 60, 0.1);
            border-radius: 4px;
        }

        /* Footer Styles */
        .footer {
            background-color: var(--emergency-dark);
            color: white;
            padding: 2rem 0;
            text-align: center;
            margin-top: auto;
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
            color: var(--warning-yellow);
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
            
            .emergency-container {
                padding: 1.5rem;
                margin: 1rem;
            }
            
            .emergency-title {
                font-size: 1.5rem;
            }
        }
    </style>
</head>
<body>
    <!-- Emergency Alert Bar -->
    <div class="emergency-alert-bar">
        <i class="fas fa-exclamation-triangle"></i> EMERGENCY SERVICES - CALL 911 FOR IMMEDIATE LIFE-THREATENING SITUATIONS
    </div>

    <!-- Top Info Bar -->
    <div class="top-info-bar">
        <div class="container">
            <div class="top-info-content">
                <div class="contact-info">
                    <a href="tel:+1234567890"><i class="fas fa-phone-alt"></i> EMERGENCY: +1 (234) 567-8900</a>
                    <a href="mailto:emergency@mediconnect.com"><i class="fas fa-envelope"></i> emergency@mediconnect.com</a>
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
    <main class="main-content">
        <div class="emergency-container">
            <h1 class="emergency-title">Emergency Assistance Request</h1>
            <p class="emergency-subtitle">Please provide all required information for immediate response</p>
            
            <form id="form1" runat="server">
                <div class="form-group">
                    <label for="TextBox1" class="required">First Name:</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="TextBox2" class="required">Last Name:</label>
                    <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" required></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="TextBox3" class="required">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" required></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="TextBox4" class="required">Describe your emergency:</label>
                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" CssClass="form-control text-area" required></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label for="TextBox5" class="required">Your location/physical address:</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" required></asp:TextBox>
                </div>
                
                <asp:Button ID="btnRequest" runat="server" Text="Request Emergency Assistance" CssClass="btn-emergency" OnClick="btnRequest_Click" />
                
                <div class="assurance-message">
                    <i class="fas fa-ambulance"></i> We will be there ASAP! Emergency team has been alerted.
                </div>
            </form>
        </div>
    </main>

    <!-- Footer -->
    <footer class="footer">
        <div class="container">
            <div class="footer-links">
                <a href="/About">About Us</a>
                <a href="/Emergency">Emergency Services</a>
                <a href="/Contact">Contact</a>
                <a href="/Privacy">Privacy Policy</a>
                <a href="/Terms">Terms of Service</a>
            </div>
            <div class="copyright">
                &copy; <script>document.write(new Date().getFullYear())</script> MediConnect Emergency Services. All rights reserved.
            </div>
        </div>
    </footer>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>