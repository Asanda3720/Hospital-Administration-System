using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Emergency
{
    public partial class RequestEmergency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connectionstring);
            cnn.Open(); //open connection

            SqlCommand comm = new SqlCommand($"INSERT INTO Emergency(Name, Surname, Email, Reason, Address, Status) VALUES (@name, @surname, @email, @reason, @address, @status)", cnn);
            comm.Parameters.AddWithValue("@name", txtName.Text);
            comm.Parameters.AddWithValue("@surname", txtSurname.Text);
            comm.Parameters.AddWithValue("@email", txtEmail.Text);


            comm.Parameters.AddWithValue("@reason", txtReason.Text);
            comm.Parameters.AddWithValue("@address", txtAddress.Text);
            comm.Parameters.AddWithValue("@status", "Pending");
            comm.ExecuteNonQuery();

            HttpCookie emergencyCookie = new HttpCookie("emergencyInfo"); //store cookie
            emergencyCookie["location"] = txtAddress.Text;
            Response.Cookies.Add(emergencyCookie);
            Response.Redirect($"/Web_Forms/Emergency/ConfirmedEmergency.aspx"); //go to  page
            cnn.Close(); //close connection
        }
    }
}