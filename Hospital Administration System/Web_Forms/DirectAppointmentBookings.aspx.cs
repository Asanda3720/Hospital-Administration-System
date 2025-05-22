using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using Hospital_Administration_System.Models;
using Hospital_Administration_System.Web_Forms.Meal;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Hospital_Administration_System.Web_Forms
{
    public partial class DirectAppointmentBookings : System.Web.UI.Page
    {
        //connections
        String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
        SqlConnection cnn;
        protected Booking CurrentBooking { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                string email = userCookieRetriever["email"];

                if (getActiveAppointment(email) == false)
                {

                    btnNewAppointment.Visible = true;
                    btnCancel.Visible = false;
                    btnReschedule.Visible = false;
                    btnOrderMeals.Visible = false;
                    btnDownloadFile.Visible = false;
                }
                else
                {

                    LoadAppointments(getActiveAppointmentId(email));
                    btnNewAppointment.Visible = false;
                    btnCancel.Visible = true;
                    btnReschedule.Visible = true;
                    btnOrderMeals.Visible = true;
                    btnDownloadFile.Visible = true;
                    string id = getActiveAppointmentId(email);
                    if (HasRescheduledAppointment(id))
                    {
                        btnReschedule.Enabled = false;
                        pnlInactiveAppointment.Visible = true;
                        

                    }
                    if (HasAdmittedPatient(id))
                    {
                        btnOrderMeals.Visible = true;

                        if (HasOrderedFood(id))
                        {
                            btnOrderMeals.Text = "View Your Meals";
                            btnOrderMeals.PostBackUrl = $"/Web_Forms/Meal/YourMeals.aspx?appid={id}";
                        }
                    }
                    else
                    {
                        btnOrderMeals.Visible = false;
                    }
                        if (HasPresribedMed(id))
                        {
                            btnCollectMed.Visible = true;
                            btnCollectMed.PostBackUrl = $"/Web_Forms/Payment/CollectMedication.aspx?appid={id}";
                        }
                    
                    

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                           "alert('Login to book an appointment!'); window.location='/Web_Forms/Login.aspx';", true);
            }
        }
        protected void btnCollectMed_Click(object sender, EventArgs e)
        {

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
               
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                cnn = new SqlConnection(connectionstring);
                cnn.Open();
                string id = null;
                SqlCommand cmd = new SqlCommand("SELECT u.User_ID, a.AppID FROM Users u LEFT JOIN Appointments a ON a.User_ID = u.User_ID WHERE Email = @email AND Status = 'Active'", cnn);
                cmd.Parameters.AddWithValue("@email", userCookieRetriever["email"]);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(1).ToString();
                }

                reader.Close();//close reader
                Response.Redirect($"/Web_Forms/Appointment/CancelAppointment.aspx?id={id}");
            }

        }

        protected void btnReschedule_Click(object sender, EventArgs e)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                cnn = new SqlConnection(connectionstring);
                cnn.Open();
                string id = null;
                SqlCommand cmd = new SqlCommand("SELECT u.User_ID, a.AppID FROM Users u LEFT JOIN Appointments a ON a.User_ID = u.User_ID WHERE Email = @email AND Status = 'Active'", cnn);
                cmd.Parameters.AddWithValue("@email", userCookieRetriever["email"]);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(1).ToString();
                }

                reader.Close();//close reader
                Response.Redirect($"/Web_Forms/Appointment/RescheduleAppointment.aspx?appid={id}");
            }
        }
        public bool getActiveAppointment(string email)
        {
            bool exists = false;
            try
            {
                //check if username exists
                cnn = new SqlConnection(connectionstring);
                cnn.Open();
               
                SqlCommand cmd = new SqlCommand("SELECT u.User_ID, a.AppID FROM Users u LEFT JOIN Appointments a ON a.User_ID = u.User_ID WHERE u.Email = @email AND a.Status = 'Active'", cnn);
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    exists = true;
                }

                reader.Close();//close reader

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
            return exists;
        }
        public string getActiveAppointmentId(string email)
        {
            string exists = null;
            try
            {
                //check if username exists
                cnn = new SqlConnection(connectionstring);
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT u.User_ID, a.AppID FROM Users u LEFT JOIN Appointments a ON a.User_ID = u.User_ID WHERE Email = @email AND a.Status = 'Active'", cnn);
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    exists = reader.GetValue(1).ToString();
                }

                reader.Close();//close reader

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
            return exists;
        }

        protected void btnNewAppointment_Click(object sender, EventArgs e)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                cnn = new SqlConnection(connectionstring);
                cnn.Open();
                string id = null;
                SqlCommand cmd = new SqlCommand("SELECT User_ID FROM Users WHERE Email = @email", cnn);
                cmd.Parameters.AddWithValue("@email", userCookieRetriever["email"]);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }

                reader.Close();//close reader
                Response.Redirect($"/Web_Forms/Appointment/NewAppointment.aspx?id={id}");
            }
        }
        private void LoadAppointments(string id)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    
                        SqlCommand cmd = new SqlCommand("SELECT Category, Speciality, FORMAT(Date, 'dd-MMMM-yyyy') AS 'Date', Time, Status, Progress FROM Appointments WHERE AppID = @appId AND Status = 'Active'", conn);
                        cmd.Parameters.AddWithValue("@appId", id);
                        conn.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the DataTable to the GridView
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    conn.Close();

                }
            }
        }

        protected void btnOrderMeals_Click(object sender, EventArgs e)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                string email = userCookieRetriever["email"];
                string id = getActiveAppointmentId(email);
                
                if (HasOrderedFood(id))
                {
                    //btnOrderMeals.Text = "View Your Meals";
                    btnOrderMeals.PostBackUrl = $"/Web_Forms/Meal/YourMeals.aspx?appid={id}";
                }
                Response.Redirect($"/Web_Forms/Meal/OrderMeals.aspx?appid={id}");
            }
        }
        
        bool HasRescheduledAppointment(string id)
        {
            string query = "SELECT 1 FROM Appointments WHERE AppID = @ID AND Rescheduled = '1'";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                
                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }
        bool HasOrderedFood(string id)
        {
            string query = "SELECT 1 FROM AppointmentMeals WHERE appID = @ID";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }
        bool HasPresribedMed(string id)
        {
            string query = "SELECT 1 FROM Prescription WHERE appID = @ID AND Collected = 'No'";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }
        bool HasAdmittedPatient(string id)
        {
            string query = "SELECT admitID FROM Admitted_Patients WHERE appID = @id AND discharged = 'No'";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }

        protected void btnDownloadFile_Click(object sender, EventArgs e)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                string email = userCookieRetriever["email"];
                string id = getActiveAppointmentId(email);
                int.TryParse(id, out int result);

                GenerateAppointmentDetailsPdf(result);
            }
        }
        public void GenerateAppointmentDetailsPdf(int appID)
        {
            // Get all data for the appointment
            DataTable appointmentData = GetAppointmentData(appID);
            DataTable labResults = GetLabResults(appID);
            DataTable prescriptions = GetPrescriptions(appID);
            DataTable xrayResults = GetXrayResults(appID);
            DataTable patientInfo = GetPatientInfo(appID);

            if (appointmentData.Rows.Count == 0)
            {
                throw new Exception("Appointment not found");
            }

            // Create PDF document
            Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 40f);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                // Fonts
                iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, new BaseColor(42, 157, 143));
                iTextSharp.text.Font headerFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                iTextSharp.text.Font sectionFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                iTextSharp.text.Font normalFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                iTextSharp.text.Font smallFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);

                // Add logo
                string logoPath = Server.MapPath("~/Images/mediConnectLogo.png");
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(150f, 60f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(logo);
                }

                pdfDoc.Add(new Paragraph(" ")); // spacer

                // Title
                Paragraph title = new Paragraph("APPOINTMENT DETAILS", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(title);

                // QR Code
                string appointmentUrl = $"https://hospitaladministrationsystemappservice.azurewebsites.net/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={appID}";
                BarcodeQRCode qrCode = new BarcodeQRCode(appointmentUrl, 150, 150, null);
                iTextSharp.text.Image qrCodeImage = qrCode.GetImage();
                qrCodeImage.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(qrCodeImage);

                Paragraph qrCaption = new Paragraph("Scan this QR code to access your appointment details", smallFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(qrCaption);

                // Patient Information
                Paragraph patientHeader = new Paragraph("PATIENT INFORMATION", headerFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f
                };
                pdfDoc.Add(patientHeader);

                if (patientInfo.Rows.Count > 0)
                {
                    DataRow patient = patientInfo.Rows[0];
                    PdfPTable patientTable = new PdfPTable(2)
                    {
                        WidthPercentage = 100,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        SpacingAfter = 20f
                    };
                    patientTable.SetWidths(new float[] { 30f, 70f });

                    AddTableRow(patientTable, "Patient Name:", $"{patient["Name"]} {patient["Surname"]}", sectionFont, normalFont);
                    AddTableRow(patientTable, "Gender:", patient["Gender"].ToString(), sectionFont, normalFont);
                    AddTableRow(patientTable, "Email:", patient["Email"].ToString(), sectionFont, normalFont);
                    AddTableRow(patientTable, "Phone:", patient["Phone"].ToString(), sectionFont, normalFont);

                    pdfDoc.Add(patientTable);
                }

                // Appointment Information
                Paragraph appointmentHeader = new Paragraph("APPOINTMENT DETAILS", headerFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f
                };
                pdfDoc.Add(appointmentHeader);

                DataRow appointment = appointmentData.Rows[0];
                PdfPTable appointmentTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    SpacingAfter = 20f
                };
                appointmentTable.SetWidths(new float[] { 30f, 70f });

                AddTableRow(appointmentTable, "Category:", appointment["Category"].ToString(), sectionFont, normalFont);
                AddTableRow(appointmentTable, "Speciality:", appointment["Speciality"].ToString(), sectionFont, normalFont);
                AddTableRow(appointmentTable, "Doctor:", appointment["Doctor"].ToString(), sectionFont, normalFont);
                AddTableRow(appointmentTable, "Date:", Convert.ToDateTime(appointment["Date"]).ToString("dd MMMM yyyy"), sectionFont, normalFont);
                AddTableRow(appointmentTable, "Time:", appointment["Time"].ToString(), sectionFont, normalFont);
                AddTableRow(appointmentTable, "Status:", appointment["Status"].ToString(), sectionFont, normalFont);
                AddTableRow(appointmentTable, "Progress:", appointment["Progress"].ToString(), sectionFont, normalFont);

                if (!string.IsNullOrEmpty(appointment["Notes"].ToString()))
                {
                    AddTableRow(appointmentTable, "Notes:", appointment["Notes"].ToString(), sectionFont, normalFont);
                }

                pdfDoc.Add(appointmentTable);

                // Lab Results
                if (labResults.Rows.Count > 0)
                {
                    Paragraph labHeader = new Paragraph("LAB RESULTS", headerFont)
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    pdfDoc.Add(labHeader);

                    foreach (DataRow lab in labResults.Rows)
                    {
                        PdfPTable labTable = new PdfPTable(2)
                        {
                            WidthPercentage = 100,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            SpacingAfter = 15f
                        };
                        labTable.SetWidths(new float[] { 30f, 70f });

                        AddTableRow(labTable, "Overview:", lab["Overview"].ToString(), sectionFont, normalFont);
                        AddTableRow(labTable, "Results:", lab["Results"].ToString(), sectionFont, normalFont);

                        pdfDoc.Add(labTable);
                    }
                }

                // Prescriptions
                if (prescriptions.Rows.Count > 0)
                {
                    Paragraph prescriptionHeader = new Paragraph("PRESCRIPTIONS", headerFont)
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    pdfDoc.Add(prescriptionHeader);

                    foreach (DataRow prescription in prescriptions.Rows)
                    {
                        PdfPTable prescriptionTable = new PdfPTable(2)
                        {
                            WidthPercentage = 100,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            SpacingAfter = 15f
                        };
                        prescriptionTable.SetWidths(new float[] { 30f, 70f });

                        AddTableRow(prescriptionTable, "Medication:", prescription["MedicationName"].ToString(), sectionFont, normalFont);
                        AddTableRow(prescriptionTable, "Dosage:", prescription["Intake"].ToString(), sectionFont, normalFont);
                        AddTableRow(prescriptionTable, "Frequency:", prescription["Times"].ToString(), sectionFont, normalFont);
                        AddTableRow(prescriptionTable, "Instructions:", prescription["Description"].ToString(), sectionFont, normalFont);
                        AddTableRow(prescriptionTable, "Collected:", prescription["Collected"].ToString(), sectionFont, normalFont);

                        pdfDoc.Add(prescriptionTable);
                    }
                }

                // X-Ray Results
                if (xrayResults.Rows.Count > 0)
                {
                    Paragraph xrayHeader = new Paragraph("X-RAY RESULTS", headerFont)
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f
                    };
                    pdfDoc.Add(xrayHeader);

                    foreach (DataRow xray in xrayResults.Rows)
                    {
                        PdfPTable xrayTable = new PdfPTable(2)
                        {
                            WidthPercentage = 100,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            SpacingAfter = 15f
                        };
                        xrayTable.SetWidths(new float[] { 30f, 70f });

                        AddTableRow(xrayTable, "Body Part:", xray["Body_Part"].ToString(), sectionFont, normalFont);
                        AddTableRow(xrayTable, "Overview:", xray["Overview"].ToString(), sectionFont, normalFont);
                        AddTableRow(xrayTable, "Report:", xray["Report"].ToString(), sectionFont, normalFont);

                        pdfDoc.Add(xrayTable);
                    }
                }

                // Footer
                Paragraph footer = new Paragraph("Thank you for choosing our healthcare services.", normalFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 30f
                };
                pdfDoc.Add(footer);

                Paragraph contactInfo = new Paragraph("Contact: info@mediconnect.com | Phone: (123) 456-7890", smallFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                pdfDoc.Add(contactInfo);

                // Page Number
                PdfContentByte cb = writer.DirectContent;
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 10);
                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "MediConnect", pdfDoc.PageSize.Width / 2, 30, 0);
                cb.EndText();

                pdfDoc.Close();

                // Output
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", $"attachment; filename=Appointment_Details_{appID}.pdf");
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
        }

        // Helper method to add rows to tables
        private void AddTableRow(PdfPTable table, string label, string value, iTextSharp.text.Font labelFont, iTextSharp.text.Font valueFont)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 5f
            };

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 5f
            };

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }

        // Database access methods (you'll need to implement these according to your data access layer)
        private DataTable GetAppointmentData(int appID)
        {
            DataTable dt = new DataTable();
            String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Category, Speciality, Doctor, Date, Time, Status, 
                     Rescheduled, Progress, Notes 
              FROM Appointments 
              WHERE AppID = @AppID", conn);

                cmd.Parameters.AddWithValue("@AppID", appID);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Close();
            }

            return dt;
        }

        private DataTable GetLabResults(int appID)
        {
            DataTable dt = new DataTable();
            String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Overview, Results 
              FROM LabResults 
              WHERE AppID = @AppID", conn);

                cmd.Parameters.AddWithValue("@AppID", appID);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Close();
            }

            return dt;
        }

        private DataTable GetPrescriptions(int appID)
        {
            DataTable dt = new DataTable();
            String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT MedicationName, Intake, Times, Description, Collected 
              FROM Prescription 
              WHERE appID = @AppID", conn);

                cmd.Parameters.AddWithValue("@AppID", appID);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Close();
            }

            return dt;
        }

        private DataTable GetXrayResults(int appID)
        {
            DataTable dt = new DataTable();
            String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Overview, Report, Body_Part 
              FROM XRAY 
              WHERE appID = @AppID", conn);

                cmd.Parameters.AddWithValue("@AppID", appID);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Close();
            }

            return dt;
        }

        private DataTable GetPatientInfo(int appID)
        {
            DataTable dt = new DataTable();
            String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT u.Name, u.Surname, u.Gender, u.Email, u.Phone 
              FROM Users u
              INNER JOIN Appointments a ON u.User_ID = a.User_ID
              WHERE a.AppID = @AppID", conn);

                cmd.Parameters.AddWithValue("@AppID", appID);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Close();
            }

            return dt;
        }
    }
}