using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using eden_food.Models;

namespace eden_food.Controllers {
    
    public class ChangeCustomerController : Controller
    {
        [HttpGet]
        public ActionResult GetCustomerInfo(string customer_name)
        {
            // Establish a connection to our database:
            var connectionString = "Data Source=./eden_db.db;Version=3;";
            var connection = new SQLiteConnection(connectionString);
            List<Customer> current_customer = new List<Customer>();
            connection.Open();

            var cat_command = new SQLiteCommand($"SELECT * FROM Customer WHERE CustName = '{customer_name}'", connection);
            var categories = cat_command.ExecuteReader();
            while (categories.Read())
            {
                current_customer.Add(new Customer
                {
                    CustName = categories["CustName"].ToString(),
                    CustomerId = Convert.ToInt32(categories["CustomerId"]),
                    Mobile = categories["Mobile"].ToString(),
                    TableNo = Convert.ToInt32(categories["TableNo"]),
                    StatusInd = Convert.ToInt32(categories["StatusInd"]),
                    ArriveDt = Convert.ToDateTime(categories["ArriveDt"])
                });
            }
            connection.Close();
            return Json(current_customer);
        }

        public ActionResult GetCustomerOrder(string customer_name)
        {
            // Establish a connection to our database:
            var connectionString = "Data Source=./eden_db.db;Version=3;";
            var connection = new SQLiteConnection(connectionString);
            int customer_id = 0;
            List<Dictionary<string, string>> order_information = new List<Dictionary<string, string>>(3);
            connection.Open();

            // Find the customer's ID here:
            var find_customer_id = new SQLiteCommand($"SELECT * FROM Customer WHERE CustName = '{customer_name}'", connection);
            var id_results = find_customer_id.ExecuteReader();
            while (id_results.Read())
            {
                customer_id = Convert.ToInt32(id_results["CustomerId"]);
            }

            var cat_command = new SQLiteCommand($"SELECT OrderItem.CustId, OrderItem.MenuCode, MenuItem.MenuTitle, MenuItem.Price, OrderItem.Qty FROM OrderItem JOIN MenuItem ON OrderItem.MenuCode = MenuItem.MenuCode WHERE OrderItem.CustId = {customer_id}", connection);
            var ordered_items = cat_command.ExecuteReader();
            while (ordered_items.Read())
            {
                Dictionary<string, string> temp = new Dictionary<string, string>(3);
                temp.Add("quantity", ordered_items["Qty"].ToString());
                temp.Add("item_name", ordered_items["MenuTitle"].ToString());
                temp.Add("unit_price", ordered_items["Price"].ToString());
                order_information.Add(temp);
            }
            connection.Close();
            return Json(order_information);
        }
    }
}