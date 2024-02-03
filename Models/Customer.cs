using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eden_food.Models
{
    public partial class Customer
    {
        public Customer()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int CustomerId { get; set; }
        public DateTime ArriveDt { get; set; }
        public string Mobile { get; set; } = null!;
        public string CustName { get; set; } = null!;
        public int TableNo { get; set; }  
        public int StatusInd { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
