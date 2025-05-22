<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectAppointmentBookings.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.DirectAppointmentBookings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appointment Details - MediConnect</title>
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
        .section__container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 2rem;
            min-height: calc(100vh - 200px);
        }
        
        .section__title {
            margin-bottom: 1.5rem;
        }
        
        .section__text {
            font-size: 2rem;
            font-weight: 600;
            margin-bottom: 0.5rem;
        }
        
        .section__text span {
            color: var(--primary-color);
        }
        
        .section__separator {
            height: 1px;
            background-color: #dee2e6;
            margin: 1.5rem 0;
        }
        
        .background__text {
            background-color: #f1f1f1;
            padding: 0.5rem 1rem;
            border-radius: 4px;
            margin: 0;
        }
        
        .text__bold {
            font-weight: 600;
        }
        
        .mt-5 {
            margin-top: 3rem !important;
        }
        
        .mt-4 {
            margin-top: 1.5rem !important;
        }
        
        .d-flex {
            display: flex !important;
        }
        
        .gap-5 {
            gap: 3rem !important;
        }
        
        .align-items-center {
            align-items: center !important;
        }
        
        .text-uppercase {
            text-transform: uppercase !important;
        }
        
        .btn {
            display: inline-flex;
            align-items: center;
            padding: 0.5rem 1.5rem;
            border-radius: 4px;
            font-weight: 500;
            text-decoration: none;
            transition: all 0.3s ease;
            border: none;
            cursor: pointer;
        }
        
        .btn-primary {
            background-color: var(--primary-color);
            color: white;
        }
        
        .btn-primary:hover {
            background-color: #21867a;
        }
        
        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }
        
        .btn-secondary:hover {
            background-color: #5a6268;
        }
        
        .btn__icon {
            margin-left: 0.5rem;
        }
        
        .alert {
            padding: 0.75rem 1.25rem;
            margin-bottom: 1rem;
            border: 1px solid transparent;
            border-radius: 0.25rem;
        }
        
        .alert-success {
            color: #155724;
            background-color: #d4edda;
            border-color: #c3e6cb;
        }
        
        .alert-danger {
            color: #721c24;
            background-color: #f8d7da;
            border-color: #f5c6cb;
        }
        
        .row {
            display: flex;
            flex-wrap: wrap;
            margin-right: -15px;
            margin-left: -15px;
        }
        
        .col-md-2 {
            flex: 0 0 16.666667%;
            max-width: 16.666667%;
            padding-right: 15px;
            padding-left: 15px;
        }
        
        .col-md-4 {
            flex: 0 0 33.333333%;
            max-width: 33.333333%;
            padding-right: 15px;
            padding-left: 15px;
        }
        
        .p-0 {
            padding: 0 !important;
        }
        
        .m-0 {
            margin: 0 !important;
        }
        
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin: 1rem 0;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            overflow: hidden;
        }
        
        .grid-view th {
            background-color: var(--primary-color);
            color: white;
            font-weight: 500;
            padding: 12px 15px;
            text-align: left;
        }
        
        .grid-view td {
            padding: 12px 15px;
            border-bottom: 1px solid #e9ecef;
            background-color: white;
        }
        
        .grid-view tr:nth-child(even) td {
            background-color: #f8f9fa;
        }
        
        .grid-view tr:hover td {
            background-color: #e9f5ff;
        }
        
        .grid-view .pager {
            background-color: #f8f9fa;
            padding: 10px;
            text-align: center;
        }
        
        .grid-view .pager a {
            color: var(--primary-color);
            padding: 5px 10px;
            text-decoration: none;
            border-radius: 4px;
            margin: 0 2px;
        }
        
        .grid-view .pager a:hover {
            background-color: var(--primary-color);
            color: white;
        }
        
        .grid-view .pager span {
            font-weight: bold;
            color: white;
            background-color: var(--primary-color);
            padding: 5px 10px;
            border-radius: 4px;
            margin: 0 2px;
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
            
            .section__container {
                padding: 1.5rem;
            }
            
            .d-flex.gap-5 {
                flex-direction: column;
                gap: 1rem !important;
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
    <form id="form1" runat="server">
        <div class="section__container">
            <div class="section__title">
                <div class="section__text">Appointment <span>Details.</span></div>
                <p>The full details of your appointment.<asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                </p>
            </div>
            <div class="section__separator"></div>
            <div class="mt-5 row">
                <div class="row align-items-center">
                    <div class="col-md-2">
                        <asp:GridView ID="GridView1" runat="server" CssClass="grid-view" GridLines="None" 
                            HeaderStyle-CssClass="grid-header" 
                            RowStyle-CssClass="grid-row" 
                            AlternatingRowStyle-CssClass="grid-alt-row"
                            PagerStyle-CssClass="pager">
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                            <AlternatingRowStyle CssClass="grid-alt-row" />
                            <PagerStyle CssClass="pager" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="section__separator"></div>
            <h5 class="text-uppercase text__bold">Action</h5>
            
            <asp:Panel ID="pnlActiveAppointment" runat="server">
                <p>Modify your appointment booking in a way that will suit you.</p>
                <asp:Label ID="lblSuccessMessage" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
                <br />
                <asp:Button ID="btnNewAppointment" runat="server" CssClass="btn btn-secondary" OnClick="btnNewAppointment_Click" Text="Book New Appointment" Visible="False" />
                <div class="mt-4 d-flex gap-5 align-items-center">
                    <div class="cancel__appointment">
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" OnClick="btnCancel_Click" Text="Cancel Appointment" />
                    </div>
                    <div class="reschedule__appointment">
                        <asp:Button ID="btnReschedule" runat="server" CssClass="btn btn-secondary" OnClick="btnReschedule_Click" Text="Reschedule Appointment" />
                       
                    </div>
                    <div class="reschedule__appointment">
                        <asp:Button ID="btnOrderMeals" runat="server" CssClass="btn btn-secondary" OnClick="btnOrderMeals_Click" Text="Order Meal" Visible="False" />
                    </div>
                    <div class="reschedule__appointment">
    <asp:Button ID="btnCollectMed" runat="server" CssClass="btn btn-secondary" OnClick="btnCollectMed_Click" Text="Collect Medication" Visible="False" />
</div>
                    <div class="cancel__appointment">
    <asp:Button ID="btnDownloadFile" runat="server" CssClass="btn btn-primary" OnClick="btnDownloadFile_Click" Text="Download your File" Visible="False" />
</div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlInactiveAppointment" runat="server" Visible="false">
                <p class="alert alert-danger">This appointment has been modified, you can modify your appointment once.</p>
            </asp:Panel>
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