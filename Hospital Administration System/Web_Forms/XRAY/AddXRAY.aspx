<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddXRAY.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.XRAY.AddXRAY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book X-Ray - MediConnect</title>
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

        /* X-Ray Booking Form */
        .xray-container {
            max-width: 850px;
            margin: 2.5rem auto;
            padding: 3rem;
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 0 30px rgba(0, 0, 0, 0.08);
            flex-grow: 1;
        }

        .xray-title {
            font-size: 2.2rem;
            font-weight: 600;
            color: var(--secondary-color);
            margin-bottom: 2.5rem;
            text-align: center;
            position: relative;
            padding-bottom: 0.75rem;
        }

        .xray-title:after {
            content: '';
            position: absolute;
            bottom: 0;
            left: 50%;
            transform: translateX(-50%);
            width: 100px;
            height: 4px;
            background-color: var(--primary-color);
        }

        .xray-title span {
            color: var(--primary-color);
        }

        .form-group {
            margin-bottom: 2rem;
        }

        .form-group label {
            display: block;
            font-weight: 500;
            color: var(--secondary-color);
            margin-bottom: 0.85rem;
            font-size: 1.1rem;
        }

        .form-control {
            width: 100%;
            padding: 1rem 1.5rem;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            font-size: 1.05rem;
            transition: all 0.3s;
            box-shadow: none;
        }

        .form-control:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.25rem rgba(42, 157, 143, 0.15);
            outline: none;
        }

        textarea.form-control {
            min-height: 140px;
            resize: vertical;
        }

        .error-message {
            color: var(--danger-color);
            font-size: 0.95rem;
            margin-top: 0.75rem;
            display: block;
        }

        .button-group {
            display: flex;
            justify-content: space-between;
            margin-top: 3rem;
            gap: 2rem;
        }

        .btn {
            padding: 1rem 2rem;
            font-size: 1.1rem;
            font-weight: 500;
            border-radius: 8px;
            cursor: pointer;
            transition: all 0.3s;
            border: none;
            min-width: 180px;
            text-align: center;
            flex: 1;
        }

        .btn-back {
            background-color: #6c757d;
            color: white;
        }

        .btn-back:hover {
            background-color: #5a6268;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        .btn-book {
            background-color: var(--primary-color);
            color: white;
        }

        .btn-book:hover {
            background-color: #21867a;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        /* Radio Button Styles */
        .radio-group {
            display: flex;
            gap: 3rem;
            margin-top: 1rem;
        }

        .radio-option {
            display: flex;
            align-items: center;
            gap: 0.75rem;
        }

        .radio-option input[type="radio"] {
            width: 20px;
            height: 20px;
            accent-color: var(--primary-color);
        }

        /* Calendar Styles */
        .calendar-container {
            position: relative;
            margin-top: 1rem;
        }

        .calendar {
            width: 100%;
            border-collapse: collapse;
            background-color: white;
            box-shadow: 0 0 10px rgba(0,0,0,0.05);
            border-radius: 8px;
            overflow: hidden;
        }

        .calendar th {
            background-color: var(--primary-color);
            color: white;
            padding: 10px;
            text-align: center;
            font-weight: 500;
        }

        .calendar td {
            padding: 10px;
            text-align: center;
            border: 1px solid #f0f0f0;
            cursor: pointer;
            transition: all 0.2s;
        }

        .calendar td:hover {
            background-color: #f5f5f5;
        }

        .calendar .title {
            background-color: white;
            color: var(--secondary-color);
            font-weight: bold;
            font-size: 1.1rem;
            border: none;
        }

        .calendar .today {
            background-color: var(--accent-color);
            color: var(--secondary-color);
            font-weight: 600;
        }

        .calendar .selected {
            background-color: var(--secondary-color);
            color: white;
            font-weight: 600;
        }

        .calendar .other-month {
            color: #b0b0b0;
            background-color: #f9f9f9;
        }

        .calendar .nextprev {
            color: var(--secondary-color);
            font-weight: 600;
            cursor: pointer;
        }

        .calendar .nextprev:hover {
            color: var(--primary-color);
        }

        /* Footer */
        .footer {
            background-color: var(--secondary-color);
            color: white;
            padding: 3rem;
            text-align: center;
            margin-top: auto;
        }

        .footer-links {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 2rem;
            margin-bottom: 2rem;
        }

        .footer-links a {
            color: white;
            text-decoration: none;
            transition: color 0.3s;
            font-size: 1.05rem;
        }

        .footer-links a:hover {
            color: var(--accent-color);
        }

        .copyright {
            font-size: 0.95rem;
            opacity: 0.8;
        }

        /* Responsive adjustments */
        @media (max-width: 992px) {
            .xray-container {
                max-width: 90%;
                padding: 2.5rem;
            }
        }

        @media (max-width: 768px) {
            .xray-container {
                padding: 2rem;
                margin: 2rem auto;
            }
            
            .top-info-content {
                justify-content: center;
                text-align: center;
            }
            
            .contact-info a {
                display: block;
                margin: 0 0 0.75rem 0;
            }
            
            .button-group {
                flex-direction: column;
                gap: 1.5rem;
            }
            
            .btn {
                width: 100%;
                padding: 1rem;
            }

            .radio-group {
                flex-direction: column;
                gap: 1rem;
            }

            .xray-title {
                font-size: 2rem;
            }
        }

        @media (max-width: 576px) {
            .xray-container {
                padding: 1.75rem;
                margin: 1.5rem auto;
            }

            .xray-title {
                font-size: 1.8rem;
            }

            .form-control {
                padding: 0.9rem 1.25rem;
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

    <!-- X-Ray Booking Form -->
    <div class="xray-container">
        <h1 class="xray-title">Book <span>X-Ray</span></h1>
        
        <form id="form1" runat="server">
            <div class="form-group">
                <label for="txtFullName">Patient:</label>
                <asp:TextBox ID="txtFullName" PlaceHolder="Patient Full Name" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="drdBodyPart">Body Part to Scan:</label>
                <asp:DropDownList ID="drdBodyPart" runat="server" CssClass="form-control">
                    <asp:ListItem>--Select Body Part--</asp:ListItem>
                    <asp:ListItem>Skull (AP/PA)</asp:ListItem>
                    <asp:ListItem>Skull (Lateral)</asp:ListItem>
                    <asp:ListItem>Facial Bones</asp:ListItem>
                    <asp:ListItem>Mandible</asp:ListItem>
                    <asp:ListItem>Sinuses</asp:ListItem>
                    <asp:ListItem>Orbits</asp:ListItem>
                    <asp:ListItem>Nasal Bones</asp:ListItem>
                    <asp:ListItem>Cervical Spine (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Cervical Spine (Oblique)</asp:ListItem>
                    <asp:ListItem>Chest (PA)</asp:ListItem>
                    <asp:ListItem>Chest (Lateral)</asp:ListItem>
                    <asp:ListItem>Ribs (Left/Right)</asp:ListItem>
                    <asp:ListItem>Sternum</asp:ListItem>
                    <asp:ListItem>Thoracic Spine</asp:ListItem>
                    <asp:ListItem>Abdomen (AP)</asp:ListItem>
                    <asp:ListItem>Abdomen (Erect)</asp:ListItem>
                    <asp:ListItem>Kidneys, Ureters, Bladder (KUB)</asp:ListItem>
                    <asp:ListItem>Lumbar Spine (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Lumbar Spine (Oblique)</asp:ListItem>
                    <asp:ListItem>Sacrum</asp:ListItem>
                    <asp:ListItem>Coccyx</asp:ListItem>
                    <asp:ListItem>Shoulder (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Clavicle</asp:ListItem>
                    <asp:ListItem>Scapula</asp:ListItem>
                    <asp:ListItem>Humerus</asp:ListItem>
                    <asp:ListItem>Elbow (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Forearm</asp:ListItem>
                    <asp:ListItem>Wrist (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Hand (PA/Oblique)</asp:ListItem>
                    <asp:ListItem>Fingers</asp:ListItem>
                    <asp:ListItem>Thumb</asp:ListItem>
                    <asp:ListItem>Pelvis (AP)</asp:ListItem>
                    <asp:ListItem>Hip (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Femur</asp:ListItem>
                    <asp:ListItem>Knee (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Patella</asp:ListItem>
                    <asp:ListItem>Tibia/Fibula</asp:ListItem>
                    <asp:ListItem>Ankle (AP/Lateral)</asp:ListItem>
                    <asp:ListItem>Foot (AP/Oblique)</asp:ListItem>
                    <asp:ListItem>Calcaneus</asp:ListItem>
                    <asp:ListItem>Toes</asp:ListItem>
                    <asp:ListItem>Chest (Portable)</asp:ListItem>
                    <asp:ListItem>Abdomen (Decubitus)</asp:ListItem>
                    <asp:ListItem>Swimmer's View (Cervical)</asp:ListItem>
                    <asp:ListItem>Odontoid View</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblError" runat="server" CssClass="error-message" Text="Please specify body part!" Visible="False"></asp:Label>
            </div>
            
            <div class="form-group">
                <label for="txtAppDate">Appointment Date:</label>
                <div class="calendar-container">
                    <asp:Calendar ID="calAppDate" runat="server" CssClass="calendar"
                        BackColor="White" BorderColor="White" BorderWidth="1px" 
                        Font-Names="Poppins" Font-Size="9pt" ForeColor="Black" 
                        Height="190px" NextPrevFormat="FullMonth" Width="100%">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle CssClass="nextprev" Font-Bold="True" 
                            Font-Size="10pt" ForeColor="#264653" VerticalAlign="Middle" />
                        <OtherMonthDayStyle CssClass="other-month" ForeColor="#b0b0b0" />
                        <SelectedDayStyle CssClass="selected" BackColor="#264653" 
                            ForeColor="White" />
                        <TitleStyle CssClass="title" BackColor="White" BorderColor="White" 
                            BorderWidth="1px" Font-Bold="True" Font-Size="12pt" 
                            ForeColor="#264653" />
                        <TodayDayStyle CssClass="today" BackColor="#e9c46a" />
                        <WeekendDayStyle BackColor="#f8f8f8" />
                    </asp:Calendar>
                </div>
                <asp:Label ID="lblErrorCalendar" runat="server" Text="Invalid Date!" CssClass="error-message" Visible="false"></asp:Label>
            </div>
            
            <div class="form-group">
                <label>Priority:</label>
                <div class="radio-group">
                    <div class="radio-option">
                        <asp:RadioButton ID="RadioButton1" runat="server" Text="Normal" Checked="True" GroupName="Priority" ValidationGroup="Priority" />
                    </div>
                    <div class="radio-option">
                        <asp:RadioButton ID="RadioButton2" runat="server" Text="Emergency" GroupName="Priority" ValidationGroup="Priority" />
                    </div>
                </div>
                <asp:Label ID="lblErrorPriority" runat="server" Text="Choose Priority!" CssClass="error-message" Visible="false"></asp:Label>
            </div>
            
            <div class="form-group">
                <label for="drdDoctor">Doctor:</label>
                <asp:DropDownList ID="drdDoctor" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:Label ID="lblErrorDoctor" runat="server" Text="Choose Doctor!" CssClass="error-message" Visible="false"></asp:Label>
            </div>
            
            <div class="form-group">
                <label for="txtNotes">Notes:</label>
                <asp:TextBox ID="txtNotes" PlaceHolder="Add Hints for Radiology Doctor" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
            
            <div class="button-group">
                <asp:Button ID="btnBack" runat="server" Text="&lt;- Back" CssClass="btn btn-back" OnClick="btnBack_Click" />
                <asp:Button ID="btnBook" runat="server" Text="Book Appointment" CssClass="btn btn-book" OnClick="btnBook_Click" />
            </div>
            
            <div class="form-group">
                <asp:Label ID="lblErrorAvailability" runat="server" Text="Doc not available for that date!" CssClass="error-message" Visible="false"></asp:Label>
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
            <div class="copyright">
                &copy; <script>document.write(new Date().getFullYear())</script> MediConnect. All rights reserved.
            </div>
        </div>
    </footer>

    <!-- Bootstrap JS Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>