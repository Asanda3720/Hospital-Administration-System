<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RescheduleAppointment.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Appointment.RescheduleAppointment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MEDICONNECT - New Appointment</title>
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

        /* Form Styles */
        .page-title {
            font-size: 24px;
            margin-bottom: 20px;
            color: var(--secondary-color);
            font-weight: 600;
        }

        .form-control {
            margin-bottom: 15px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            padding: 10px 15px;
        }

        .form-control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.2rem rgba(42, 157, 143, 0.25);
        }

        .m-t-20 {
            margin-top: 20px;
        }

        .text-danger {
            color: #dc3545;
            font-size: 14px;
        }

        .btn-primary {
            background-color: var(--primary-color);
            border-color: var(--primary-color);
            padding: 10px 25px;
            font-weight: 500;
            border-radius: 50px;
        }

        .btn-primary:hover {
            background-color: #21867a;
            border-color: #21867a;
        }

        /* Responsive adjustments */
        @media (max-width: 992px) {
            .top-info-content {
                justify-content: center;
                gap: 1rem;
            }

            .contact-info {
                text-align: center;
                margin-bottom: 0.5rem;
            }

            .search-box {
                margin-bottom: 0.5rem;
            }
        }

        @media (max-width: 576px) {
            .contact-info a {
                display: block;
                margin-right: 0;
                margin-bottom: 0.25rem;
            }

            .navbar-brand {
                font-size: 1.5rem;
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
                <div class="search-box">
                    <input type="text" placeholder="Search..." />
                    <button type="submit"><i class="fas fa-search"></i></button>
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

    <form id="form1" runat="server">
        <div class="container py-5">
            <div class="row">
                <div class="col-lg-8 offset-lg-2">
                    <h4 class="page-title">New Booking Category</h4>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 offset-lg-2">
                    <div class="card shadow-sm" style="left: 0px; top: 0px">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtName">Appointment Category:</label>
                                        <br />
                                        <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                        <br />
                                        <br />
                                        <label for="txtName">Doctor Speciality:</label><br />
                                        <asp:TextBox ID="txtSpeciality" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                        <br />
                                        <br />
                                        <label for="txtName">Doctor&#39;s Name:</label><asp:TextBox ID="txtDocName" runat="server" CssClass="form-control" placeholder="Dentist" Enabled="False"></asp:TextBox>
                                        <label for="txtName">Appointment Date:<asp:Label ID="lblErrorDate" runat="server" ForeColor="Red" Text="Choose a valid date!" Visible="False"></asp:Label>
                                        </label><br />
                                        <asp:Calendar ID="calAppDate" runat="server" AutoPostBack="False" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                                            <TodayDayStyle BackColor="#CCCCCC" />
                                        </asp:Calendar>
                                        <br />
                                        <label for="txtName">Appointment Time:</label><br />
                                        <label for="txtName">
                                        <asp:DropDownList ID="drdAppTime" runat="server" Height="30px" Width="70px">
                                            <asp:ListItem Selected="True">08:00</asp:ListItem>
                                            <asp:ListItem>08:30</asp:ListItem>
                                            <asp:ListItem>09:00</asp:ListItem>
                                            <asp:ListItem>09:30</asp:ListItem>
                                            <asp:ListItem>10:00</asp:ListItem>
                                            <asp:ListItem>10:30</asp:ListItem>
                                            <asp:ListItem>11:00</asp:ListItem>
                                            <asp:ListItem>11:30</asp:ListItem>
                                            <asp:ListItem>12:00</asp:ListItem>
                                            <asp:ListItem>12:30</asp:ListItem>
                                            <asp:ListItem>13:00</asp:ListItem>
                                            <asp:ListItem>13:30</asp:ListItem>
                                            <asp:ListItem>14:00</asp:ListItem>
                                            <asp:ListItem>14:30</asp:ListItem>
                                            <asp:ListItem>15:00</asp:ListItem>
                                            <asp:ListItem>15:30</asp:ListItem>
                                            <asp:ListItem>16:30</asp:ListItem>
                                        </asp:DropDownList>
                                        </label>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="m-t-20 text-end">
                                <asp:Button ID="btnSubmit" runat="server" Text="Reschedule Appointment" 
                                    CssClass="btn btn-primary submit-btn" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

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

    <!-- Bootstrap JS Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
