<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPasswordVerification.aspx.cs" Inherits="Hospital_Administration_System.Web_Forms.Account.ForgotPasswordVerification" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Verify Code - MediConnect</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/font-awesome.min.css" rel="stylesheet" />
    <link href="~/Content/bootstrap-icons.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600&display=swap" rel="stylesheet">
    <style type="text/css">
        body {
            font-family: 'Poppins', sans-serif;
            background-color: rgba(0, 0, 0, 0.5);
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
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
            color: #2c3e50;
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
            border-color: #3498db;
            box-shadow: 0 0 0 3px rgba(52, 152, 219, 0.2);
            outline: none;
        }

        .btn-verify {
            background: linear-gradient(135deg, #3498db, #2980b9);
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
            color: #3498db;
            font-size: 0.9rem;
            cursor: pointer;
            padding: 5px;
            transition: all 0.2s;
        }

        .btn-resend:hover {
            color: #2980b9;
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

        .input-group {
            position: relative;
            margin-bottom: 20px;
        }

        .input-icon {
            position: absolute;
            left: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: #bdc3c7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
        
        <div class="verification-modal">
            <div class="modal-header">
                <h2 class="modal-title">Verify Your Email<asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                </h2>
                <p class="modal-subtitle">We've sent a verification code to your email address</p>
            </div>
            
            <div class="verification-code-input">
                <div class="input-group">
                    <i class="fa fa-shield-alt input-icon"></i>
                    <asp:TextBox ID="txtVerificationCode" runat="server" CssClass="code-input" 
                        placeholder="Enter 6-digit code" MaxLength="6"></asp:TextBox>
                </div>
                
                <asp:Label ID="lblMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                
                <asp:Button ID="btnVerify" runat="server" Text="Verify Code" CssClass="btn-verify" 
                    OnClick="btnVerify_Click" />
            </div>
            
            <div class="resend-section">
                <asp:Button ID="btnResend" runat="server" Text="Resend Code" CssClass="btn-resend" 
                    OnClick="btnResend_Click" Enabled="false" />
                <div id="timerContainer" class="timer-text" runat="server">Resend available in <span id="timeLeftSpan">02:00</span></div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        // Timer functionality
        var timeLeft = 120; // 2 minutes in seconds
        var timerId;

        function startTimer() {
            timerId = setInterval(function () {
                timeLeft--;

                var minutes = Math.floor(timeLeft / 60);
                var seconds = timeLeft % 60;

                // Add leading zeros
                minutes = minutes < 10 ? "0" + minutes : minutes;
                seconds = seconds < 10 ? "0" + seconds : seconds;

                document.getElementById('timeLeftSpan').innerHTML = minutes + ":" + seconds;

                if (timeLeft <= 0) {
                    clearInterval(timerId);
                    document.getElementById('<%= btnResend.ClientID %>').disabled = false;
                    document.getElementById('<%= timerContainer.ClientID %>').style.display = 'none';
                }
            }, 1000);
        }

        // Start the timer when page loads
        window.onload = function () {
            startTimer();

            // Focus on the code input field
            document.getElementById('<%= txtVerificationCode.ClientID %>').focus();
        };

        // Function to handle resend button click
        function resendCode() {
            // Disable the button and restart timer
            document.getElementById('<%= btnResend.ClientID %>').disabled = true;
            timeLeft = 120;
            document.getElementById('<%= timerContainer.ClientID %>').style.display = 'block';
            startTimer();

            // Call server-side method to resend code
            PageMethods.ResendVerificationCode(onResendSuccess, onResendFailure);
        }

        function onResendSuccess(result) {
            // Show success message
            document.getElementById('<%= lblMessage.ClientID %>').innerText = "New verification code sent!";
            document.getElementById('<%= lblMessage.ClientID %>').className = "success-message";
            document.getElementById('<%= lblMessage.ClientID %>').style.display = "block";
        }
        
        function onResendFailure(error) {
            // Show error message
            document.getElementById('<%= lblMessage.ClientID %>').innerText = "Failed to resend code. Please try again.";
            document.getElementById('<%= lblMessage.ClientID %>').className = "error-message";
            document.getElementById('<%= lblMessage.ClientID %>').style.display = "block";
        }
    </script>
</body>
</html>