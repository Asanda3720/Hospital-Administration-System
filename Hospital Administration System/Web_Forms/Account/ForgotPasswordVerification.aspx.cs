using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Account
{
    public partial class ForgotPasswordVerification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize the verification code on first load
                if (Session["VerificationCode"] == null)
                {

                    string user = Request.QueryString["user"];
                    Session["VerificationCode"] = GenerateVerificationCode();
                    sendMail(user, Session["VerificationCode"].ToString());
                    //Label1.Text = " - " + Session["VerificationCode"].ToString();

                }
                lblMessage.Visible = false;
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            
            
            string enteredCode = txtVerificationCode.Text.Trim();
            string storedCode = Session["VerificationCode"] as string;
            HttpCookie resetCookie = Request.Cookies["resetInfo"]; //store cookie

            string id = Request.QueryString["id"];

            if (string.IsNullOrEmpty(enteredCode))
            {
                ShowErrorMessage("Please enter the verification code");
                return;
            }

            if (enteredCode == storedCode)
            {
                
                    String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(connectionstring);
                    cnn.Open();


                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Password = @password WHERE  User_ID = @id", cnn);
                    cmd.Parameters.AddWithValue("@password", resetCookie["password"].ToString());
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = cmd;
                    adapter.UpdateCommand.ExecuteNonQuery();
                    cnn.Close();


                    Session.Remove("VerificationCode");
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Password Successfully Reset! Please Login'); window.location='/Web_Forms/Login.aspx';", true);

                

            }
            else
            {
                ShowErrorMessage("Incorrect verification code. Please try again.");
            }
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            // Disable the button immediately
            btnResend.Enabled = false;

            // Generate and store new code
            string newCode = GenerateVerificationCode();
            Session["VerificationCode"] = newCode;

            // In a real application, you would send this code via email
            // SendVerificationEmail(newCode);

            // Show success message
            lblMessage.Text = "New verification code sent to your email!";
            lblMessage.CssClass = "success-message";
            lblMessage.Visible = true;

            // Register script to restart timer on client side
            ScriptManager.RegisterStartupScript(this, GetType(), "restartTimer",
                "timeLeft = 120; startTimer(); document.getElementById('" + timerContainer.ClientID + "').style.display = 'block';", true);
        }

        private string GenerateVerificationCode()
        {
            // Generate a 6-digit random number
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void ShowErrorMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "error-message";
            lblMessage.Visible = true;
        }

        [WebMethod]
        public static string ResendVerificationCode()
        {
            // This method can be called from client-side via PageMethods
            // Implement your actual resend logic here
            return "Verification code resent successfully";
        }
        public void sendMail(string email, string verificationCode)
        {
            try
            {


                string subject = "Your Secure Verification Code";
                string body = $@"
<html>
<head>
    <style>
        body {{ 
            font-family: 'Arial', sans-serif; 
            line-height: 1.6;
            color: #333333;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
        }}
        .header {{
            color: #2c3e50;
            border-bottom: 1px solid #eeeeee;
            padding-bottom: 10px;
        }}
        .code {{
            font-size: 24px;
            font-weight: bold;
            color: #e74c3c;
            letter-spacing: 2px;
            margin: 20px 0;
            padding: 10px 15px;
            background-color: #f9f9f9;
            border-left: 4px solid #e74c3c;
            display: inline-block;
        }}
        .footer {{
            margin-top: 30px;
            font-size: 12px;
            color: #7f8c8d;
            border-top: 1px solid #eeeeee;
            padding-top: 10px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h2>Account Verification</h2>
        </div>
        
        <p>Dear Valued User,</p>
        
        <p>Thank you for using our services. Please use the following verification code to complete your authentication:</p>
        
        <div class='code'>{verificationCode}</div>
        
        <p>This code is valid for a limited time and should not be shared with anyone, including our support team.</p>
        
        <p>If you did not request this code, please ignore this message or contact our support team immediately.</p>
        
        <div class='footer'>
            <p>For your security, this email was automatically generated. Please do not reply.</p>
            <p>&copy; {DateTime.Now.Year} MediConnect. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sibisibornface@gmail.com", "Hospital Administration");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                // Attach the PDF from the memory stream
                //Attachment pdfAttachment = new Attachment(pdfStream, "Invoice.pdf", "application/pdf");
                //mail.Attachments.Add(pdfAttachment);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) // Change to 587
                {
                    smtp.Credentials = new NetworkCredential("sibisibornface@gmail.com", "ofleicpmxgjnfrlr");
                    smtp.EnableSsl = true; // Required for STARTTLS on port 587
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(mail);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", $"alert('{ex.Message}');", true);
            }
        }

    }
}