<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelAppointment.aspx.cs" 
    Inherits="Hospital_Administration_System.Web_Forms.Appointment.CancelAppointment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cancel Appointment</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f8f9fa;
            padding: 20px;
        }
        .confirmation-modal {
            max-width: 500px;
            margin: 0 auto;
            background: white;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
            padding: 30px;
            text-align: center;
        }
        .modal-icon {
            font-size: 48px;
            color: #dc3545;
            margin-bottom: 20px;
        }
        .modal-title {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 15px;
            color: #343a40;
        }
        .modal-message {
            font-size: 16px;
            color: #6c757d;
            margin-bottom: 25px;
        }
        .btn-container {
            display: flex;
            justify-content: center;
            gap: 15px;
        }
        .btn-danger {
            background-color: #dc3545;
            border-color: #dc3545;
            padding: 8px 25px;
            font-weight: 500;
        }
        .btn-danger:hover {
            background-color: #c82333;
            border-color: #bd2130;
        }
        .btn-secondary {
            padding: 8px 25px;
            font-weight: 500;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="confirmation-modal">
            <div class="modal-icon">
                <i class="fas fa-exclamation-triangle"></i>
            </div>
            <h2 class="modal-title">Cancel Appointment</h2>
            <p class="modal-message">Are you sure you want to cancel this appointment?</p>
            <div class="btn-container">
                <asp:Button ID="btnBack" runat="server" Text="Back" 
                    CssClass="btn btn-secondary" OnClick="btnBack_Click" CausesValidation="False" />
                <asp:Button ID="btnCancelAppointment" runat="server" Text="Cancel Appointment" 
                    CssClass="btn btn-danger" OnClick="btnCancelAppointment_Click" />
            </div>
        </div>
    </form>

    <!-- Bootstrap JS Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>