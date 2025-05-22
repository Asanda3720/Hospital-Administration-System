<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmedEmergency.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Emergency.ConfirmedEmergency" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Help is Coming! - MediConnect</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&family=Roboto:wght@400;500;700&display=swap" rel="stylesheet">
    
    <style type="text/css">
        :root {
            --emergency-red: #d90429;
            --emergency-blue: #0066cc;
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
            overflow-x: hidden;
        }
        
        /* Emergency Alert Bar */
        .emergency-alert-bar {
            background-color: var(--emergency-red);
            color: white;
            padding: 0.5rem 0;
            text-align: center;
            font-weight: 700;
            font-size: 1.1rem;
            position: relative;
            z-index: 1000;
        }

        /* Header Styles */
        .top-info-bar {
            background-color: var(--emergency-dark);
            color: white;
            padding: 0.5rem 0;
            font-size: 0.9rem;
            position: relative;
            z-index: 1000;
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
            position: relative;
            z-index: 1000;
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
            position: relative;
            overflow: hidden;
        }

        .confirmation-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 2rem;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            border-left: 6px solid var(--emergency-red);
            position: relative;
            z-index: 100;
            text-align: center;
        }

        .confirmation-title {
            font-size: 2.5rem;
            font-weight: 700;
            color: var(--emergency-red);
            margin-bottom: 1.5rem;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .confirmation-message {
            font-size: 1.2rem;
            margin-bottom: 2rem;
            color: var(--emergency-dark);
        }

        .location-display {
            background-color: rgba(239, 35, 60, 0.1);
            padding: 1rem;
            border-radius: 8px;
            margin-bottom: 2rem;
            font-weight: 500;
        }

        .location-display i {
            color: var(--emergency-red);
            margin-right: 0.5rem;
        }

        .siren-light {
            width: 100%;
            height: 20px;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 10;
        }

        .light-bar {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            opacity: 0.7;
        }

        /* Ambulance Animation */
        .ambulance-container {
            position: absolute;
            bottom: 50px;
            left: -200px;
            width: 200px;
            height: 100px;
            z-index: 50;
            animation: ambulanceMove 8s linear forwards;
        }

        .ambulance {
            width: 100%;
            height: 100%;
            background-image: url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path fill="%23ffffff" d="M160 416c0 35.3 28.7 64 64 64s64-28.7 64-64V288H160v128zm304-128v128c0 17.7-14.3 32-32 32s-32-14.3-32-32V288h64zM48 384c-17.7 0-32 14.3-32 32s14.3 32 32 32 32-14.3 32-32v-64H48v32zm464-192H352V80c0-8.8-7.2-16-16-16h-64c-8.8 0-16 7.2-16 16v112H32c-17.7 0-32 14.3-32 32v96c0 17.7 14.3 32 32 32h32v64c0 35.3 28.7 64 64 64s64-28.7 64-64V288h160v128c0 35.3 28.7 64 64 64s64-28.7 64-64V288h32c17.7 0 32-14.3 32-32v-96c0-17.7-14.3-32-32-32zm-336-96h64v96h-64v-96zm-48 240c-17.7 0-32-14.3-32-32s14.3-32 32-32 32 14.3 32 32-14.3 32-32 32zm288 0c-17.7 0-32-14.3-32-32s14.3-32 32-32 32 14.3 32 32-14.3 32-32 32z"/></svg>');
            background-size: contain;
            background-repeat: no-repeat;
            background-position: center;
            position: relative;
        }

        .ambulance-lights {
            position: absolute;
            top: 10px;
            width: 100%;
            height: 20px;
            display: flex;
            justify-content: center;
            gap: 40px;
        }

        .light {
            width: 20px;
            height: 20px;
            border-radius: 50%;
        }

        @keyframes ambulanceMove {
            0% { left: -200px; }
            100% { left: calc(100% + 200px); }
        }

        @keyframes sirenFlash {
            0%, 100% { background-color: var(--emergency-red); }
            50% { background-color: var(--emergency-blue); }
        }

        .pulse {
            animation: pulse 1s infinite;
        }

        @keyframes pulse {
            0% { transform: scale(1); }
            50% { transform: scale(1.05); }
            100% { transform: scale(1); }
        }

        /* Footer Styles */
        .footer {
            background-color: var(--emergency-dark);
            color: white;
            padding: 2rem 0;
            text-align: center;
            margin-top: auto;
            position: relative;
            z-index: 1000;
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
            
            .confirmation-container {
                padding: 1.5rem;
                margin: 1rem;
            }
            
            .confirmation-title {
                font-size: 1.8rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- Emergency Alert Bar -->
    <div class="emergency-alert-bar">
        <i class="fas fa-exclamation-triangle"></i> EMERGENCY RESPONSE IN PROGRESS - HELP IS ON THE WAY
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

    <!-- Siren Lights Animation -->
    <div class="siren-light">
        <div class="light-bar" id="sirenLight"></div>
    </div>

    <!-- Main Content -->
    <main class="main-content">
        <!-- Ambulance Animation -->
        <div class="ambulance-container">
            <div class="ambulance">
                <div class="ambulance-lights">
                    <div class="light" id="light1"></div>
                    <div class="light" id="light2"></div>
                </div>
            </div>
        </div>

        <div class="confirmation-container pulse">
            <h1 class="confirmation-title"><i class="fas fa-check-circle"></i> EMERGENCY CONFIRMED</h1>
            <p class="confirmation-message">
                Our medical team has been dispatched to your location and is on the way.<br>
                Estimated arrival time: <strong>8-12 minutes</strong>
            </p>
            
            <div class="location-display">
                <i class="fas fa-map-marker-alt"></i> 
                &nbsp;<asp:Label ID="Label1" runat="server" Text="Label" OnDataBinding="Label1_DataBinding"></asp:Label>
            </div>
            
            <div class="alert alert-success" role="alert">
                <h4 class="alert-heading"><i class="fas fa-info-circle"></i> What to do now:</h4>
                <ul class="text-start">
                    <li>Stay calm and remain at the specified location</li>
                    <li>If safe, turn on outside lights to help us find you</li>
                    <li>Prepare any medications or medical information</li>
                    <li>Keep your phone nearby for possible contact</li>
                </ul>
            </div>
            
            <button class="btn btn-danger mt-3" onclick="stopSiren()">
                <i class="fas fa-volume-mute"></i> Mute Siren Sound
            </button>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
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

    <!-- Audio Element for Siren -->
    <audio id="sirenSound" loop>
        <source src="https://assets.mixkit.co/sfx/preview/mixkit-ambulance-siren-1612.mp3" type="audio/mpeg">
        Your browser does not support the audio element.
    </audio>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>

    <script>
        // Function to get cookie value
        function getCookie(name) {
            const value = `; ${document.cookie}`;
            const parts = value.split(`; ${name}=`);
            if (parts.length === 2) return parts.pop().split(';').shift();
        }

        // Set user location from cookie
        document.addEventListener('DOMContentLoaded', function () {
            // Get location from cookie (you'll need to set this in your C# code)
            const location = getCookie('userLocation') || 'Location not specified';
            document.getElementById('userLocation').textContent = location;

            // Start animations
            startAnimations();

            // Play siren sound
            document.getElementById('sirenSound').play();
        });

        // Animation functions
        function startAnimations() {
            // Siren light animation
            const sirenLight = document.getElementById('sirenLight');
            const light1 = document.getElementById('light1');
            const light2 = document.getElementById('light2');

            let isRed = true;

            setInterval(() => {
                if (isRed) {
                    sirenLight.style.backgroundColor = 'var(--emergency-blue)';
                    light1.style.backgroundColor = 'var(--emergency-blue)';
                    light2.style.backgroundColor = 'var(--emergency-red)';
                } else {
                    sirenLight.style.backgroundColor = 'var(--emergency-red)';
                    light1.style.backgroundColor = 'var(--emergency-red)';
                    light2.style.backgroundColor = 'var(--emergency-blue)';
                }
                isRed = !isRed;
            }, 300);
        }

        // Function to stop siren sound
        function stopSiren() {
            const siren = document.getElementById('sirenSound');
            siren.pause();
            document.querySelector('.btn-danger').innerHTML = '<i class="fas fa-volume-up"></i> Play Siren Sound';
            document.querySelector('.btn-danger').setAttribute('onclick', 'playSiren()');
        }

        // Function to play siren sound
        function playSiren() {
            const siren = document.getElementById('sirenSound');
            siren.play();
            document.querySelector('.btn-danger').innerHTML = '<i class="fas fa-volume-mute"></i> Mute Siren Sound';
            document.querySelector('.btn-danger').setAttribute('onclick', 'stopSiren()');
        }
    </script>
    </form>
</body>
</html>