using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Payment
{
    public partial class Billings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                LoadAppointments(userCookieRetriever["email"]);
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Web_Forms/Payment/Payment.aspx");
        }
        private void LoadAppointments(string id)
        {
            
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    // Query to get the bill details for GridView
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT 
                    b.billID,
                    b.appID as [Appointment Number],
                    b.Category,
                    b.Product,
                    b.Cost,
                    a.Date as [Appointment Date],
                    u.Name,
                    u.Surname
                FROM 
                    Billings b
                JOIN 
                    Appointments a ON b.appID = a.AppID
                JOIN 
                    Users u ON a.User_ID = u.User_ID
                WHERE 
                    u.email = @email AND
                    b.Settled = 'N'
                ORDER BY 
                    b.billID DESC",
                        conn);

                    cmd.Parameters.AddWithValue("@email", id);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    // Calculate total amount
                    decimal totalAmount = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Cost"] != DBNull.Value)
                        {
                            totalAmount += Convert.ToDecimal(row["Cost"]);
                        }
                    }
                if (totalAmount > 0)
                {
                    lblNoOutstanding.Visible = false;
                    btnPayment.Enabled = true;
                    HttpCookie paymentCookie = new HttpCookie("paymentInfo"); //store cookie
                    paymentCookie["totalAmount"] = totalAmount.ToString();
                    Response.Cookies.Add(paymentCookie);
                }
                else
                {
                    lblNoOutstanding.Visible = true;
                    btnPayment.Enabled = false;
                }
                
                lblAmount.Text = "R" + totalAmount.ToString();

                    // Or store it in ViewState for later use:
                   // ViewState["TotalAmount"] = totalAmount;
                }
            
        }
    }
}