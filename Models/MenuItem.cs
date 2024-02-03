using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eden_food.Models
{
    [Keyless]
    public partial class MenuItem
    {
        public MenuItem() 
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public string MenuCode { get; set; } = null!;
        public string MenuTitle { get; set; } = null!;
        public string CatCode { get; set; } = null!;
        public double Price { get; set; }

        // IDK why this won't work (manually add this in later)...
        // public virtual Category CatCodeNavigation { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}