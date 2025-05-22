<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUsers.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Analysis.AddUsers" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Register - MediConnect</title>
    
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

        /* Registration Form Styles */
        .registration-container {
            padding: 4rem 0;
            min-height: calc(100vh - 200px);
        }

        .auth__section {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 600px;
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

        .btn-register {
            background: linear-gradient(90deg, var(--primary-color) 0%, var(--secondary-color) 100%);
            border: none;
            padding: 10px 25px;
            border-radius: 5px;
            color: white;
            font-weight: 500;
            transition: all 0.3s;
        }

        .btn-register:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
            color: white;
        }

        .btn-secondary {
            background: #f8f9fa;
            border: 1px solid #ddd;
            color: var(--secondary-color);
            padding: 10px 25px;
            border-radius: 5px;
            font-weight: 500;
            transition: all 0.3s;
        }

        .btn-secondary:hover {
            background: #e9ecef;
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
                            <a class="nav-link" href="/Web_Forms/Home.aspx">HOME</a>
                        </li>
                        <%--<li class="nav-item">
                            <a class="nav-link" href="/Web_Forms/DirectAppointmentBookings.aspx">APPOINTMENT</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="/Web_Forms/Emergency/RequestEmergency.aspx">EMERGENCY</a>
                        </li>--%>
                        <li class="nav-item">
                            <a class="nav-link" href="/Web_Forms/Doctors.aspx">DOCTORS</a>
                        </li>
                                                    <%
                                //HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
                                if (userCookieRetriever != null)
                                {
                                    if (userCookieRetriever["userType"] == "0")
                                    {

                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Analysis/Administration.aspx'>ADMIN</a></li>");
                                        Response.Write("<li class='nav-item'><a class='nav-link active' href='/Web_Forms/Analysis/AddUsers.aspx'>ADD USERS</a></li>");
                                       Response.Write("<li class='nav-item'><a class='nav-link' href='/Web_Forms/Analysis/AllUsers.aspx'>View USERS</a></li>");
                                    }
                                }
                                
%>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <!-- Registration Form Section -->
    <div class="registration-container">
        <div class="container">
            <div class="auth__section">
                <div class="top__wrapper"></div>
                <div class="auth__header">
                    <p class="auth__page__title">Create User Account</p>
                    <p class="auth__page__subtitle">Join MediConnect to manage your healthcare needs</p>
                </div>
                
                <form id="form1" runat="server" role="form">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <asp:ValidationSummary runat="server" CssClass="alert alert-danger" DisplayMode="BulletList" />
                    
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtbxName">First Name</label>
                                <div class="input">
                                    <i class="fas fa-user"></i>
                                    <asp:TextBox ID="txtbxName" runat="server" CssClass="form-control" placeholder="Enter first name"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtbxName"
                                    CssClass="text-danger" ErrorMessage="First name is required" Display="Dynamic" />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtbxName"
                                    CssClass="text-danger" 
                                    ErrorMessage="Invalid first name. Please enter a name starting with a capital letter, followed by letters, hyphens, or apostrophes. It must be between 2 and 50 characters." 
                                    ValidationExpression="^[A-Z][a-zA-Z'-]{1,49}$" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtbxSurname">Last Name</label>
                                <div class="input">
                                    <i class="fas fa-user"></i>
                                    <asp:TextBox ID="txtbxSurname" runat="server" CssClass="form-control" placeholder="Enter last name"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtbxSurname"
                                    CssClass="text-danger" ErrorMessage="Last name is required" Display="Dynamic" />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtbxSurname"
                                    CssClass="text-danger" 
                                    ErrorMessage="Invalid last name. Please enter a name starting with a capital letter, followed by letters, hyphens, or apostrophes. It must be between 2 and 50 characters." 
                                    ValidationExpression="^[A-Z][a-zA-Z'-]{1,49}$" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="drdUserType">User Type</label>
                                <div class="input">
                                    <i class="fas fa-user-tie"></i>
                                    <asp:DropDownList ID="drdUserType" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True">Doctor</asp:ListItem>
                                        <asp:ListItem>Lab Technician</asp:ListItem>
                                        <asp:ListItem>X-Ray</asp:ListItem>
                                        <asp:ListItem>Paramedic</asp:ListItem>
                                        <asp:ListItem>Patient</asp:ListItem>
                                        <asp:ListItem>Pharmacist</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <!-- Empty column for alignment -->
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtEmail">Email Address</label>
                        <div class="input">
                            <i class="fas fa-at"></i>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" 
                                placeholder="email@example.com" TextMode="Email"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                            CssClass="text-danger" ErrorMessage="Email is required" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                            CssClass="text-danger" ErrorMessage="Please enter a valid email address"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" />
                        <asp:Label ID="lblEmailError" runat="server" CssClass="text-danger" Text="Email already exists!" Visible="False"></asp:Label>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtPhone">Phone Number</label>
                        <div class="input">
                            <i class="fas fa-phone"></i>
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" 
                                placeholder="+27761234567"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhone"
                            CssClass="text-danger" ErrorMessage="Phone number is required" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPhone"
                            ValidationExpression="^(?:\+27|0)(\d{2})\d{7}$"
                            ErrorMessage="Please enter a valid South African phone number (e.g., +27761234567 or 0721234567)"
                            CssClass="text-danger" Display="Dynamic" />
                    </div>
                    
                    <div class="form-group">
                        <label for="dlstGender">Gender</label>
                        <div class="input">
                            <i class="fas fa-venus-mars"></i>
                            <asp:DropDownList ID="dlstGender" runat="server" CssClass="form-control">
                                <asp:ListItem>--Select Gender--</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                                <asp:ListItem>Prefer not to say</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Label ID="lblMatch0" runat="server" CssClass="text-danger" Text="Gender is required!" Visible="False"></asp:Label>
                    </div>
                    
                    <div class="form-group text-center mt-4">
                        <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-register" Text="Create Account"
                            OnClick="btnRegister_Click1" />
                    </div>
                
                    <div class="section__separator"></div>
                </form>
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
    
    <!-- Custom JS -->
    <script type="text/javascript">
        // Enable Bootstrap tooltips
        $(document).ready(function () {
            $('[data-bs-toggle="tooltip"]').tooltip();
        });
    </script>
</body>
</html>