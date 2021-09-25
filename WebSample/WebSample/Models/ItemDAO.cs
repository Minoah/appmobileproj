using System;
using System.Collections.Generic;

#nullable disable

namespace WebSample.Models
{
    public partial class ItemDAO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ProductDAO Product { get; set; }
    }
}
