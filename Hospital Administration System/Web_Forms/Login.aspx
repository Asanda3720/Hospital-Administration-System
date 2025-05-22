<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login - MediConnect</title>
    
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    
    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.1/font/bootstrap-icons.css" rel="stylesheet">
    
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    
    <!-- Custom CSS -->
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

        /* Login Form Styles */
        .login-container {
            padding: 4rem 0;
            min-height: calc(100vh - 200px);
        }

        .auth__section {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 450px;
            padding: 2rem;
            margin: 0 auto;
        }

        .top__wrapper {
            height: 5px;
            background: linear-gradient(90deg, var(--primary-color) 0%, var(--secondary-color) 100%);
            border-radius: 5px 5px 0 0;
            margin: -2rem -2rem 2rem -2rem;
        }

        .auth__header {
            text-align: center;
            margin-bottom: 2rem;
        }

        .auth__page__title {
            font-size: 1.8rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 0.5rem;
        }

        .auth__page__subtitle {
            color: #666;
            margin-bottom: 0;
        }

        .form-group {
            margin-bottom: 1.5rem;
        }

        .form-group label {
            display: block;
            margin-bottom: 0.5rem;
            font-weight: 500;
            color: #555;
        }

        .input {
            position: relative;
        }

        .input i {
            position: absolute;
            left: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: #777;
        }

        .form-control {
            padding-left: 40px;
            height: 45px;
            border-radius: 5px;
            border: 1px solid #ddd;
            width: 100%;
        }

        .form-control:focus {
            box-shadow: none;
            border-color: var(--primary-color);
        }

        .btn-login {
            background: linear-gradient(90deg, var(--primary-color) 0%, var(--secondary-color) 100%);
            border: none;
            padding: 10px 25px;
            border-radius: 5px;
            color: white;
            font-weight: 500;
            transition: all 0.3s;
        }

        .btn-login:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
            color: white;
        }

        .btn__icon {
            margin-left: 8px;
        }

        .section__separator {
            height: 1px;
            background-color: #eee;
            margin: 2rem 0;
        }

        .text-danger {
            color: #dc3545 !important;
            font-size: 0.875rem;
            margin-top: 0.25rem;
        }

        .useful-links {
            text-align: center;
            margin-bottom: 1rem;
            font-weight: 500;
            color: var(--secondary-color);
        }

        .link-container {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 1rem;
        }

        .link-container a {
            color: var(--primary-color);
            text-decoration: none;
            transition: all 0.3s;
        }

        .link-container a:hover {
            color: var(--secondary-color);
            text-decoration: underline;
        }

        /* Verification Modal Styles */
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: rgba(0, 0, 0, 0.5);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
            display: none;
        }

        .verification-modal {
            background: white;
            border-radius: 12px;
            box-shadow: 0 5px 20px rgba(0, 0, 0, 0.15);
            width: 100%;
            max-width: 400px;
            padding: 30px;
            text-align: center;
            animation: fadeIn 0.3s ease-out;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(-20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .modal-header {
            margin-bottom: 20px;
        }

        .modal-title {
            font-size: 1.5rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 8px;
        }

        .modal-subtitle {
            color: #7f8c8d;
            font-size: 0.9rem;
            margin-bottom: 0;
        }

        .verification-code-input {
            margin: 20px 0;
        }

        .code-input {
            width: 100%;
            padding: 12px 15px;
            border: 2px solid #e0e0e0;
            border-radius: 8px;
            font-size: 1rem;
            text-align: center;
            letter-spacing: 5px;
            font-weight: 500;
            transition: all 0.3s;
        }

        .code-input:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 3px rgba(42, 157, 143, 0.2);
            outline: none;
        }

        .btn-verify {
            background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
            color: white;
            border: none;
            padding: 12px 24px;
            border-radius: 8px;
            font-size: 1rem;
            font-weight: 500;
            cursor: pointer;
            width: 100%;
            transition: all 0.3s;
        }

        .btn-verify:hover {
            opacity: 0.9;
            transform: translateY(-2px);
        }

        .resend-section {
            margin-top: 20px;
            padding-top: 20px;
            border-top: 1px solid #eee;
        }

        .btn-resend {
            background: none;
            border: none;
            color: var(--primary-color);
            font-size: 0.9rem;
            cursor: pointer;
            padding: 5px;
            transition: all 0.2s;
        }

        .btn-resend:hover {
            color: var(--secondary-color);
            text-decoration: underline;
        }

        .btn-resend:disabled {
            color: #95a5a6;
            cursor: not-allowed;
            text-decoration: none;
        }

        .timer-text {
            color: #95a5a6;
            font-size: 0.8rem;
            margin-top: 5px;
        }

        .error-message {
            color: #e74c3c;
            font-size: 0.9rem;
            margin-top: 10px;
            min-height: 20px;
        }

        .success-message {
            color: #2ecc71;
            font-size: 0.9rem;
            margin-top: 10px;
            min-height: 20px;
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
            
            .auth__section {
                padding: 1.5rem;
            }
            
            .link-container {
                flex-direction: column;
                gap: 0.5rem;
            }
        }
    </style>
    
    <link rel="shortcut icon" href="assets/img/favicon.ico" type="image/x-icon" />
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
                <a href="Register.aspx" class="login-link"><i class="fas fa-user-plus"></i> Register</a>
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
<%--<li class="nav-item">
    <a class="nav-link" href="/Web_Forms/DirectAppointmentBookings.aspx">APPOINTMENT</a>
</li>--%>
<li class="nav-item">
    <a class="nav-link" href="/Web_Forms/Emergency/RequestEmergency.aspx">EMERGENCY</a>
</li>
<li class="nav-item">
    <a class="nav-link" href="/Web_Forms/Doctors.aspx">DOCTORS</a>
</li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <!-- Login Form Section -->
    <div class="login-container">
        <div class="container">
            <div class="auth__section">
                <div class="top__wrapper"></div>
                <div class="auth__header">
                    <p class="auth__page__title">Welcome Back</p>
                    <p class="auth__page__subtitle">Sign in to access your account</p>
                </div>
                
                <form id="form1" runat="server" role="form">
                    <asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
                    
                    <asp:ValidationSummary runat="server" CssClass="alert alert-danger" DisplayMode="BulletList" style="left: 0px; top: 3px" />
                    
                    <div class="form-group">
                        <label for="txtEmail">Email Address</label>
                        <div class="input">
                            <i class="fas fa-at"></i>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                                placeholder="email@example.com" TextMode="Email"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                            CssClass="text-danger" ErrorMessage="Email is required" Display="Dynamic" />
                    </div>
                    
                    <div class="form-group">
                        <label for="txtPassword">Password</label>
                        <div class="input">
                            <i class="fas fa-lock"></i>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" 
                                CssClass="form-control" placeholder="Enter your password"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                            CssClass="text-danger" ErrorMessage="Password is required" Display="Dynamic" />
                        <span class="btn__icon">
                           
                        <asp:Label ID="lblMatch" runat="server" ForeColor="Red" Text="Invalid credentials!" Visible="False"></asp:Label>
                        
                        </span>
                    </div>
                    
                    <div class="form-group form-check">
                        <asp:CheckBox ID="cbRememberMe" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label" for="cbRememberMe">Remember me</label>
                    </div>
                    
                    <div class="form-group text-center mt-4">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-login"
                            OnClick="btnLogin_Click" />
                        <span class="btn__icon">
                            <i class="fas fa-arrow-right">
                        <br />
                        <br />
                        </i>
                        </span>
                    </div>
                </form>
                
                <div class="section__separator"></div>
                
                <div class="link-container">
                    <a href="/Web_Forms/Account/ForgotPassword.aspx">Forgot Password?</a>
                    <a href="Register.aspx">Create Account</a>
                    <a href="/">Back to Home</a>
                </div>
            </div>
        </div>
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