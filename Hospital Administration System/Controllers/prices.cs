using Hospital_Administration_System.Models;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace Hospital_Administration_System.Controllers
{
    public class prices
    {
        private static readonly string filePath = HttpContext.Current.Server.MapPath("~/price.json");

        public static priceModel LoadData()
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<priceModel>(json);
        }

        public static void SaveData(priceModel data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
