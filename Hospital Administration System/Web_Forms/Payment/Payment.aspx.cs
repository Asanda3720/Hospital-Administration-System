using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using Stripe;

namespace Hospital_Administration_System.Web_Forms.Payment
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string total = Request.QueryString["total"];
                if (!string.IsNullOrEmpty(total))
                {
                    amount.Text = total; // Assign to the TextBox
                }
            }
        }



        [WebMethod]
        public static object CreatePaymentIntent(string amount)
        {
            try
            {
                // Set your secret key from web.config
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];

                // Create a PaymentIntent with the order amount and currency
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(decimal.Parse(amount) * 100), // Convert to cents
                    Currency = "zar",
                    Description = "Hospital Payment",
                    Metadata = new System.Collections.Generic.Dictionary<string, string>
                    {
                        { "integration_check", "accept_a_payment" }
                    }
                };

                var service = new PaymentIntentService();
                var intent = service.Create(options);

                return new { clientSecret = intent.ClientSecret };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                settleBills(userCookieRetriever["email"]);
            }
        }

    public void settleBills(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty");
            }

            string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // First get the bill IDs that will be updated
                List<int> billIdsToSettle = new List<int>();

                // Get unsettled bills for this email
                string selectQuery = @"SELECT b.billID
                             FROM Billings b
                             JOIN Appointments a ON b.appID = a.AppID
                             JOIN Users u ON a.User_ID = u.User_ID
                             WHERE u.email = @email AND b.Settled = 'N'";

                SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@email", email);

                conn.Open();
                using (SqlDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billIdsToSettle.Add(reader.GetInt32(0));
                    }
                }

                if (billIdsToSettle.Count == 0)
                {
                    throw new Exception("No unsettled bills found for this email");
                }

                // Now update these bills
                string updateQuery = @"UPDATE Billings 
                             SET SettledDate = GETDATE(),
                                 Settled = 'Y'
                             WHERE billID IN (" + string.Join(",", billIdsToSettle) + ")";

                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Generate invoice ONLY for these specific bills
                    GenerateInvoicePdf(email, billIdsToSettle);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                    $"alert('Payment Successful! See downloads for your invoice.'); window.location.href = '/Web_Forms/Payment/Billings.aspx';", true);
            }
        }

        public void GenerateInvoicePdf(string email, List<int> billIds)
        {
            // Get billing data for SPECIFIC bill IDs
            List<BillingRecord> billingRecords = GetBillingRecords(email, billIds);

            if (billingRecords.Count == 0)
            {
                throw new Exception("No billing records found for this email");
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
                iTextSharp.text.Font normalFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                iTextSharp.text.Font smallFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                iTextSharp.text.Font boldFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);

                // Add logo
                //string logoPath = Server.MapPath("~/Images/mediConnectLogo.png");
                //if (System.IO.File.Exists(logoPath))
                //{
                //    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                //    logo.ScaleToFit(150f, 60f);
                //    logo.Alignment = Element.ALIGN_CENTER;
                //    pdfDoc.Add(logo);
                //}

                pdfDoc.Add(new Paragraph(" ")); // spacer

                // Title
                Paragraph title = new Paragraph("INVOICE RECEIPT", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(title);

                // Patient Info
                PdfPTable infoTable = new PdfPTable(2)
                {
                    WidthPercentage = 80,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };
                infoTable.SetWidths(new float[] { 30f, 70f });

                AddTableRow(infoTable, "Patient:", $"{billingRecords[0].PatientName}", headerFont, normalFont);
                AddTableRow(infoTable, "Email:", email, headerFont, normalFont);
                AddTableRow(infoTable, "Invoice Date:", DateTime.Now.ToString("dd MMMM yyyy"), headerFont, normalFont);
                AddTableRow(infoTable, "Invoice Number:", $"INV-{DateTime.Now:yyyyMMdd}-{billingRecords[0].BillID}", headerFont, normalFont);
                pdfDoc.Add(infoTable);

                // Billing Items Table
                PdfPTable billingTable = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    SpacingBefore = 20f,
                    SpacingAfter = 20f
                };
                billingTable.SetWidths(new float[] { 10f, 40f, 20f, 30f });

                // Table headers
                AddBillingTableHeader(billingTable, "No.", boldFont);
                AddBillingTableHeader(billingTable, "Description", boldFont);
                AddBillingTableHeader(billingTable, "Category", boldFont);
                AddBillingTableHeader(billingTable, "Amount", boldFont);

                // Add billing items
                decimal total = 0;
                for (int i = 0; i < billingRecords.Count; i++)
                {
                    var record = billingRecords[i];
                    AddBillingTableRow(billingTable, (i + 1).ToString(), normalFont);
                    AddBillingTableRow(billingTable, record.Product, normalFont);
                    AddBillingTableRow(billingTable, record.Category, normalFont);
                    AddBillingTableRow(billingTable, record.Cost.ToString("C"), normalFont);
                    total += record.Cost;
                }

                pdfDoc.Add(billingTable);

                // Total
                PdfPTable totalTable = new PdfPTable(2)
                {
                    WidthPercentage = 50,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    SpacingBefore = 10f
                };
                totalTable.SetWidths(new float[] { 30f, 70f });

                AddTableRow(totalTable, "Subtotal:", total.ToString("C"), boldFont, boldFont);
                AddTableRow(totalTable, "Tax (0%):", "R0.00", boldFont, boldFont);
                AddTableRow(totalTable, "Total:", total.ToString("C"), boldFont, boldFont);

                pdfDoc.Add(totalTable);

                // Payment confirmation
                Paragraph paymentConfirmation = new Paragraph("Payment Status: PAID", headerFont)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingBefore = 20f
                };
                pdfDoc.Add(paymentConfirmation);

                Paragraph paymentDate = new Paragraph($"Paid on: {DateTime.Now:dd MMMM yyyy HH:mm}", normalFont)
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                pdfDoc.Add(paymentDate);

                // Footer
                Paragraph footer = new Paragraph("Thank you for your payment.", normalFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 30f
                };
                pdfDoc.Add(footer);

                Paragraph contactInfo = new Paragraph("For any queries, please contact: billing@hospital.com | Phone: (123) 456-7890", smallFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                pdfDoc.Add(contactInfo);

                pdfDoc.Close();

                // Output
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Hospital_Invoice.pdf");
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
        }

        // Helper class to hold billing data
        private class BillingRecord
        {
            public int BillID { get; set; }
            public string PatientName { get; set; }
            public string Category { get; set; }
            public string Product { get; set; }
            public decimal Cost { get; set; }
        }

        // Helper method to get billing records
        private List<BillingRecord> GetBillingRecords(string email, List<int> billIds)
        {
            List<BillingRecord> records = new List<BillingRecord>();

            if (billIds.Count == 0) return records;

            string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT b.billID, b.Category, b.Product, b.Cost, 
                        u.FirstName + ' ' + u.LastName as PatientName
                        FROM Billings b
                        JOIN Appointments a ON b.appID = a.AppID
                        JOIN Users u ON a.User_ID = u.User_ID
                        WHERE u.email = @email 
                        AND b.billID IN (" + string.Join(",", billIds) + ")";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", email);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new BillingRecord
                        {
                            BillID = reader.GetInt32(0),
                            Category = reader.GetString(1),
                            Product = reader.GetString(2),
                            Cost = reader.GetDecimal(3),
                            PatientName = reader.GetString(4)
                        });
                    }
                }
            }

            return records;
        }

        // Helper method for info table rows
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

        // Helper method for billing table headers
        private void AddBillingTableHeader(PdfPTable table, string text, iTextSharp.text.Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = new BaseColor(230, 230, 230),
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5f
            };
            table.AddCell(cell);
        }

        // Helper method for billing table rows
        private void AddBillingTableRow(PdfPTable table, string text, iTextSharp.text.Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font))
            {
                Padding = 5f
            };
            table.AddCell(cell);
        }

    }
}