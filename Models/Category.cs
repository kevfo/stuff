using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eden_food.Models
{
    [Keyless]
    public partial class Category
    {
        public Category() { 
            MenuItem = new HashSet<MenuItem>();
        }

        public string CatCode { get; set; } = null!;
        public string CatTitle { get; set; } = null!;
        public int CatOrder { get; set; }

        public virtual ICollection<MenuItem> MenuItem { get; set; }
    }
}
