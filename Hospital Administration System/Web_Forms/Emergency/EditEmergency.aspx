<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEmergency.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Emergency.EditEmergency" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Emergency - MediConnect</title>
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
            --danger-color: #e76f51;
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

        .login-link {
            color: white;
            text-decoration: none;
        }

        .login-link:hover {
            color: var(--accent-color);
        }

        /* Emergency Form Styles */
        .emergency-container {
            max-width: 800px;
            margin: 2rem auto;
            padding: 2rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }

        .emergency-title {
            font-size: 1.8rem;
            font-weight: 600;
            color: var(--danger-color);
            margin-bottom: 1.5rem;
            border-bottom: 2px solid var(--danger-color);
            padding-bottom: 0.5rem;
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

        .form-control.textarea {
            min-height: 100px;
            resize: vertical;
        }

        .btn-update {
            background-color: var(--danger-color);
            color: white;
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            font-weight: 500;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.3s;
            border: none;
            min-width: 180px;
            text-align: center;
        }

        .btn-update:hover {
            background-color: #d45c3e;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
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
            
            .emergency-container {
                padding: 1.5rem;
                margin: 1rem;
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
                                    if (userCookieRetriever["userType"] == "5")
                                    {

                                       Response.Write("<li class='nav-item'><a class='nav-link active' href='/Web_Forms/Emergency/AllEmergencies.aspx'> ALL EMERGENCIES</a></li>");
                                    }
                                }
                                
%>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    <!-- Main Content -->
    <div class="emergency-container">
        <h1 class="emergency-title">Edit Emergency</h1>
        
        <form id="form1" runat="server">
            <div class="form-group">
                <label>Name</label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Surname</label>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Email</label>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Reason</label>
                <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" CssClass="form-control textarea" Enabled="False"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Address</label>
                <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" CssClass="form-control textarea" Enabled="False"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Update Status</label>
                <asp:DropDownList ID="drdStatus" runat="server" CssClass="form-control">
                    <asp:ListItem>--Update Status--</asp:ListItem>
                    <asp:ListItem>Resolved</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblErrorStatus" runat="server" Text="Select Status!" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            
            <div class="form-group" style="text-align: center;">
                
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-update" OnClick="btnUpdate_Click" BackColor="Red" />
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