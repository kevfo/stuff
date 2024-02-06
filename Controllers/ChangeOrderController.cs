using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using eden_food.Models;

namespace eden_food.Controllers {
    
    public class ChangeOrderController : Controller
    {
        [HttpPost]
        public ActionResult AddItem(string menu_item, string customer_id)
        {
            var connectionString = "Data Source=./eden_db.db;Version=3;";
            var connection = new SQLiteConnection(connectionString);
            string menu_code = "";
            connection.Open();

            // Find our item:
            var find_item_command = new SQLiteCommand($"SELECT * FROM MenuItem WHERE MenuTitle = '{menu_item.Trim()}'", connection);
            var code = find_item_command.ExecuteReader();
            while (code.Read())
            {
                menu_code = code["MenuCode"].ToString();
            }

            // Insert our item:
            var insert_item_command = new SQLiteCommand($"INSERT INTO OrderItem(CustId, MenuCode, Qty) VALUES ({customer_id}, '{menu_code}', 1)", connection);
            insert_item_command.ExecuteNonQuery();

            // Return stuff to print to our table:
            List<Dictionary<string, string>> order_information = new List<Dictionary<string, string>>(3);
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

        public ActionResult IncrementItem(string menu_item, string customer_id) {
            var connectionString = "Data Source=./eden_db.db;Version=3;";
            var connection = new SQLiteConnection(connectionString);
            string menu_code = "";
            List<Dictionary<string, string>> ToReturn = new List<Dictionary<string, string>>();
            connection.Open();

            // Find our menu code here:
            var find_code_command = new SQLiteCommand($"SELECT * FROM MenuItem WHERE MenuTitle = '{menu_item}'", connection);
            var find_code_execution = find_code_command.ExecuteReader();
            while (find_code_execution.Read())
            {
                menu_code = find_code_execution["MenuCode"].ToString();
            }

            // Update our value here:
            var update_order_command = new SQLiteCommand($"Update OrderItem SET Qty = Qty + 1 WHERE CustId = {customer_id} AND MenuCode = '{menu_code}'", connection);
            update_order_command.ExecuteNonQuery();

            // Get out values here:
            var fetch_customer_order = new SQLiteCommand($"SELECT OrderItem.CustId, OrderItem.MenuCode, MenuItem.MenuTitle, MenuItem.Price, OrderItem.Qty FROM OrderItem JOIN MenuItem ON OrderItem.MenuCode = MenuItem.MenuCode WHERE OrderItem.CustId = {customer_id}", connection);
            var customer_orders = fetch_customer_order.ExecuteReader();
            while (customer_orders.Read())
            {
                Dictionary<string, string> temp = new Dictionary<string, string>(3);
                temp.Add("quantity", customer_orders["Qty"].ToString());
                temp.Add("item_name", customer_orders["MenuTitle"].ToString());
                temp.Add("unit_price", customer_orders["Price"].ToString());
                ToReturn.Add(temp);
            }
            connection.Close();
            return Json(ToReturn);
        }

        public ActionResult DecrementItem(string menu_item, string customer_id)
        {
            var connectionString = "Data Source=./eden_db.db;Version=3;";
            var connection = new SQLiteConnection(connectionString);
            string menu_code = "";
            int item_quantity = 0;
            List<Dictionary<string, string>> ToReturn = new List<Dictionary<string, string>>();
            connection.Open();

            // Find our menu code here:
            var find_code_command = new SQLiteCommand($"SELECT * FROM MenuItem WHERE MenuTitle = '{menu_item}'", connection);
            var find_code_execution = find_code_command.ExecuteReader();
            while (find_code_execution.Read())
            {
                menu_code = find_code_execution["MenuCode"].ToString();
            }
            
            // Find our item's quantity here:
            var find_quantity_command = new SQLiteCommand($"SELECT * FROM OrderItem WHERE CustId = {customer_id} AND MenuCode = '{menu_code}'", connection);
            var quantity = find_quantity_command.ExecuteReader();
            while (quantity.Read())
            {
                item_quantity = Convert.ToInt32(quantity["Qty"]);
            }

            // Decide what to do with our item here:
            if (item_quantity == 1)
            {
                var delete_command = new SQLiteCommand($"DELETE FROM OrderItem WHERE CustId = {customer_id} AND MenuCode = '{menu_code}' AND Qty = 1", connection);
                delete_command.ExecuteNonQuery();
            } else
            {
                var diminish_command = new SQLiteCommand($"Update OrderItem SET Qty = Qty - 1 WHERE CustId = {customer_id} AND MenuCode = '{menu_code}'", connection);
                diminish_command.ExecuteNonQuery();
            }

            // Fetch final values here:
            var fetch_customer_order = new SQLiteCommand($"SELECT OrderItem.CustId, OrderItem.MenuCode, MenuItem.MenuTitle, MenuItem.Price, OrderItem.Qty FROM OrderItem JOIN MenuItem ON OrderItem.MenuCode = MenuItem.MenuCode WHERE OrderItem.CustId = {customer_id}", connection);
            var customer_orders = fetch_customer_order.ExecuteReader();
            while (customer_orders.Read())
            {
                Dictionary<string, string> temp = new Dictionary<string, string>(3);
                temp.Add("quantity", customer_orders["Qty"].ToString());
                temp.Add("item_name", customer_orders["MenuTitle"].ToString());
                temp.Add("unit_price", customer_orders["Price"].ToString());
                ToReturn.Add(temp);
            }
            connection.Close();
            return Json(ToReturn);
        }
    }
}