using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Payment
{
    public partial class Receipts : System.Web.UI.Page
    {
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string AppointmentCategory { get; set; }
        public string Speciality { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (id != null)
            {
                //System.Diagnostics.Debug.WriteLine("Loading appointment with ID: " + id);
                loadAppDetails(id);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AppointmentId))
            {
                GenerateAppointmentConfirmationPdf(PatientName, PatientSurname,
                    AppointmentCategory, Speciality, AppointmentDate,
                    AppointmentTime, AppointmentId);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert",
                    "alert('Appointment data not loaded properly');", true);
            }
        }

        public void GenerateAppointmentConfirmationPdf(string patientName, string patientSurname,
    string appointmentCategory, string speciality, DateTime appointmentDate,
    string appointmentTime, string appointmentId)
        {
            Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 40f);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                // Fonts (using fully-qualified Font class)
                iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, new BaseColor(42, 157, 143));
                iTextSharp.text.Font headerFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
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
                Paragraph title = new Paragraph("APPOINTMENT CONFIRMATION", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(title);

                // QR Code
                string appointmentUrl = $"https://hospitaladministrationsystemappservice.azurewebsites.net/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={appointmentId}";
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

                // Patient Info Table
                PdfPTable infoTable = new PdfPTable(2)
                {
                    WidthPercentage = 80,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };
                infoTable.SetWidths(new float[] { 30f, 70f });

                AddTableRow(infoTable, "Patient Name:", $"{patientName} {patientSurname}", headerFont, normalFont);
                AddTableRow(infoTable, "Appointment Category:", appointmentCategory, headerFont, normalFont);
                AddTableRow(infoTable, "Speciality:", speciality, headerFont, normalFont);
                AddTableRow(infoTable, "Appointment Date:", appointmentDate.ToString("dd MMMM yyyy"), headerFont, normalFont);
                AddTableRow(infoTable, "Appointment Time:", appointmentTime, headerFont, normalFont);
                AddTableRow(infoTable, "Appointment ID:", appointmentId.ToString(), headerFont, normalFont);
                pdfDoc.Add(infoTable);

                // Notes
                Paragraph notesHeader = new Paragraph("Important Information", headerFont)
                {
                    SpacingBefore = 20f
                };
                pdfDoc.Add(notesHeader);

                iTextSharp.text.List notesList = new iTextSharp.text.List(iTextSharp.text.List.UNORDERED);
                notesList.Add(new iTextSharp.text.ListItem("Please arrive 15 minutes before your appointment time.", normalFont));
                notesList.Add(new iTextSharp.text.ListItem("Bring your ID and medical insurance card (if applicable).", normalFont));
                notesList.Add(new iTextSharp.text.ListItem("Cancel or reschedule at least 24 hours in advance if needed.", normalFont));
                notesList.Add(new iTextSharp.text.ListItem("Scan the QR code above for quick access to your appointment details.", normalFont));

                pdfDoc.Add(notesList);

                // Footer
                Paragraph footer = new Paragraph("Thank you for choosing MediConnect for your healthcare needs.", normalFont)
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
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Page 1 of 1", pdfDoc.PageSize.Width / 2, 30, 0);
                cb.EndText();

                pdfDoc.Close();

                // Output
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=MediConnect_Appointment_Confirmation.pdf");
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
        }

        // Helper Method
        private void AddTableRow(PdfPTable table, string label, string value, iTextSharp.text.Font labelFont, iTextSharp.text.Font valueFont)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont))
            {
                Border = PdfPCell.NO_BORDER,
                PaddingBottom = 5f
            };
            table.AddCell(labelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont))
            {
                Border = PdfPCell.NO_BORDER,
                PaddingBottom = 5f
            };
            table.AddCell(valueCell);
        }
        public void loadAppDetails(string id)
        {
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(connectionstring))
                {
                    cnn.Open();

                    // Fixed query - assuming you want to match AppID
                    SqlCommand comm = new SqlCommand(@"SELECT a.Category, a.Speciality, a.Doctor, a.Date, a.Time, 
                                             a.Progress, a.Notes, u.Name, u.Surname, u.Email, a.AppID  
                                             FROM Appointments a 
                                             LEFT JOIN Users u ON u.User_ID = a.User_ID 
                                             WHERE a.User_ID = @appID AND Status = 'Active'", cnn);

                    comm.Parameters.AddWithValue("@appID", id);

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())  // Use if instead of while since we expect single record
                        {
                            AppointmentCategory = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                            Speciality = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);

                            if (!reader.IsDBNull(3))
                                AppointmentDate = reader.GetDateTime(3);

                            AppointmentTime = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            PatientName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                            PatientSurname = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                            AppointmentId = reader.GetValue(10).ToString();

                           // Label1.Text = AppointmentId; // Show the ID for debugging
                        }
                        else
                        {
                            // No record found - handle this case
                           // Label1.Text = "No appointment found with ID: " + id;
                        }
                    }
                }
            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           // Label1.Text = AppointmentId;
        }
    }


}