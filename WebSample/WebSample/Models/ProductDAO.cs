using System;
using System.Collections.Generic;

#nullable disable

namespace WebSample.Models
{
    public partial class ProductDAO
    {
        public ProductDAO()
        {
            Items = new HashSet<ItemDAO>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }

        public virtual ICollection<ItemDAO> Items { get; set; }
    }
}
