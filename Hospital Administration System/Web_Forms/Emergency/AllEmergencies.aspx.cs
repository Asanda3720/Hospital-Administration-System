using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Emergency
{
    public partial class AllEmergencies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPendingEmergencies();
                LoadResolvedEmergencies();
            }
        }

        private void LoadPendingEmergencies()
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT emergencyID, Name, Surname, Email, Reason, Address, Status FROM Emergency WHERE Status = 'Pending'", conn);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    conn.Close();
                }
            }
        }

        private void LoadResolvedEmergencies()
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT emergencyID, Name, Surname, Email, Reason, Address, Status FROM Emergency WHERE Status = 'Resolved'", conn);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    conn.Close();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditEmergency")
            {
                string emergencyID = e.CommandArgument.ToString();
                Response.Redirect($"~/Web_Forms/Emergency/EditEmergency.aspx?appid={emergencyID}");
            }
        }
    }
}