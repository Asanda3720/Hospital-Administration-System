<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmitPatient.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.AdmitPatient.AdmitPatient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admit Patient - MediConnect</title>
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
            --danger-color: #e76f51;
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

        /* Admission Form Styles */
        .admit-container {
            max-width: 600px;
            margin: 2rem auto;
            padding: 2rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            flex-grow: 1;
        }

        .admit-title {
            font-size: 1.8rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 1.5rem;
            text-align: center;
        }

        .admit-title span {
            color: var(--primary-color);
        }

        .form-group {
            margin-bottom: 1.5rem;
        }

        .form-group label {
            display: block;
            font-weight: 500;
            color: var(--secondary-color);
            margin-bottom: 0.5rem;
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
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.2rem rgba(42, 157, 143, 0.25);
            outline: none;
        }

        .error-message {
            color: var(--danger-color);
            font-size: 0.9rem;
            margin-top: 0.5rem;
        }

        .btn-admit {
            background-color: var(--primary-color);
            color: white;
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            font-weight: 500;
            border-radius: 4px;
            border: none;
            cursor: pointer;
            transition: all 0.3s;
            width: 100%;
        }

        .btn-admit:hover {
            background-color: #21867a;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        /* Footer */
        .footer {
            background-color: var(--secondary-color);
            color: white;
            padding: 2rem;
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
            color: var(--accent-color);
        }

        .copyright {
            font-size: 0.9rem;
            opacity: 0.8;
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .admit-container {
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
    <a class="nav-link " href="/Web_Forms/Home.aspx">HOME</a>
</li>
                        
                        <li class="nav-item">
    <a class="nav-link" href="/Web_Forms/Doctors.aspx">DOCTORS</a>
</li>             
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <!-- Admission Form -->
    <div class="admit-container">
        <h1 class="admit-title">Admit <span>Patient</span></h1>
        
        <form id="form1" runat="server">
            <div class="form-group">
                <label for="TextBox1">Number of days to be admitted for:</label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBox1" 
                    CssClass="error-message" ErrorMessage="Please enter a valid number of days!" 
                    MaximumValue="1000" MinimumValue="1" Type="Integer" Display="Dynamic"></asp:RangeValidator>
            </div>
            
            <div class="form-group">
                <label for="drdRooms">Room to be admitted to:</label>
                <asp:DropDownList ID="drdRooms" runat="server" CssClass="form-control">
                    <asp:ListItem Value="" Selected="True">-- SELECT WARD --</asp:ListItem>
                    <asp:ListItem>Ward 101</asp:ListItem>
                    <asp:ListItem>Ward 102</asp:ListItem>
                    <asp:ListItem>Ward 103</asp:ListItem>
                    <asp:ListItem>Ward 104</asp:ListItem>
                    <asp:ListItem>Ward 105</asp:ListItem>
                    <asp:ListItem>Ward 106</asp:ListItem>
                    <asp:ListItem>Ward 107</asp:ListItem>
                    <asp:ListItem>Ward 108</asp:ListItem>
                    <asp:ListItem>Ward 109</asp:ListItem>
                    <asp:ListItem>Ward 110</asp:ListItem>
                    <asp:ListItem>Ward 111</asp:ListItem>
                    <asp:ListItem>Ward 112</asp:ListItem>
                    <asp:ListItem>Ward 113</asp:ListItem>
                    <asp:ListItem>Ward 114</asp:ListItem>
                    <asp:ListItem>Ward 115</asp:ListItem>
                    <asp:ListItem>Ward 116</asp:ListItem>
                    <asp:ListItem>Ward 117</asp:ListItem>
                    <asp:ListItem>Ward 118</asp:ListItem>
                    <asp:ListItem>Ward 119</asp:ListItem>
                    <asp:ListItem>Ward 120</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblErrorRoom" runat="server" CssClass="error-message" Text="Please select a ward" Visible="False"></asp:Label>
            </div>
            
            <asp:Button ID="btnAdmit" runat="server" Text="Admit Patient" CssClass="btn-admit" OnClick="btnAdmit_Click" />
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
            <div class="copyright">
                &copy; <script>document.write(new Date().getFullYear())</script> MediConnect. All rights reserved.
            </div>
        </div>
    </footer>

    <!-- Bootstrap JS Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>