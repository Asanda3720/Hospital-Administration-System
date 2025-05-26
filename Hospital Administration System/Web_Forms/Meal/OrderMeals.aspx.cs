using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Meal
{
    public partial class OrderMeals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string selectedJson = selectedMeals.Value;
            string total = Request.Form["totalAmountInput"];

            if (string.IsNullOrWhiteSpace(selectedJson) || selectedJson == "[]")
            {
                lblErrorMeal.Visible = true;
                return;
            }

            lblErrorMeal.Visible = false;

            // Deserialize if needed
            var selected = JsonConvert.DeserializeObject<List<MealItem>>(selectedJson);

            // Example: Show names and total price (you can save to DB or process further)
            string summary = "You ordered: " + string.Join(", ", selected.Select(m => m.name)) +
                             ". Total: R" + selected.Sum(m => m.price);

            // You can display this in a Label or store in DB
            Response.Write("<script>alert('" + summary + "');</script>");
            Response.Redirect($"/Web_Forms/Payment/Payment.aspx?total={total}");
        }

        public class MealItem
        {
            public string name { get; set; }
            public int price { get; set; }
        }



    }
}
