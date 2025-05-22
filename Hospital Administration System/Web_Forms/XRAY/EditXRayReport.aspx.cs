using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.XRAY
{
    public partial class EditXRayReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["appid"];
                if (id != null)
                {
                    loadAppDetails(id);
                }
            }
        }
        public void loadAppDetails(string id)
        {
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(connectionstring))
                {
                    cnn.Open();

                    SqlCommand comm = new SqlCommand("SELECT Overview, Report, Body_Part, Image FROM XRAY WHERE AppID = @appID", cnn);
                    comm.Parameters.AddWithValue("@appID", id);

                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        TextBox9.Text = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        TextBox10.Text = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        TextBox1.Text = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);

                        if (!reader.IsDBNull(3))
                        {
                            byte[] imgData = (byte[])reader["Image"];
                            string base64String = Convert.ToBase64String(imgData);
                            string imageUrl = "data:image/png;base64," + base64String;
                            imgItem.ImageUrl = imageUrl;
                        }
                        else
                        {
                            // Display a Google Material Icon when image is null
                            imgItem.ImageUrl = null; // Clear any existing image
                            //imgItem.Attributes["class"] = "material-icons"; // Add Material Icons class
                            imgItem.AlternateText = "No image available";
                            imgItem.ToolTip = "No X-ray image available";
                            // You'll need to add literal HTML for the icon
                            // Or use a different approach if imgItem is a standard Image control
                        }
                    }
                }
            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        protected void btnUpdateReport_Click(object sender, EventArgs e)
        {
            byte[] imageBytes = null;
            if (fileUploadImage.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fileUploadImage.PostedFile.InputStream))
                {
                    imageBytes = br.ReadBytes(fileUploadImage.PostedFile.ContentLength);
                }
            }

            string id = Request.QueryString["appid"];
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(connectionstring))
                {
                    cnn.Open();

                    // Build the SQL command dynamically based on whether we have a new image
                    string updateSql = "UPDATE XRAY SET Overview = @overview, Report = @report";

                    if (imageBytes != null)
                    {
                        updateSql += ", Image = @image";
                    }

                    updateSql += " WHERE AppID = @appid";

                    SqlCommand cmd = new SqlCommand(updateSql, cnn);
                    cmd.Parameters.AddWithValue("@overview", TextBox9.Text);
                    cmd.Parameters.AddWithValue("@report", TextBox10.Text);

                    if (imageBytes != null)
                    {
                        cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = imageBytes;
                    }

                    cmd.Parameters.AddWithValue("@appid", id);

                    cmd.ExecuteNonQuery();
                }

                loadAppDetails(id);
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Report successfully updated!');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                $"alert('Report successfully updated!'); window.location='/Web_Forms/XRAY/AllXRAYS.aspx';", true);
            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {

                Response.Redirect($"/Web_Forms/XRAY/AllXRAYS.aspx");
            }
        }
    }
}