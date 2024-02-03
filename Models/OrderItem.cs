using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eden_food.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public string MenuCode { get; set; } = null!;
        public int Qty { get; set; }

        public virtual Customer Cust { get; set; } = null!;
        // Manually add this in later:
        // public virtual MenuItem MenuCodeNavigation { get; set; } = null!;
    }
}
