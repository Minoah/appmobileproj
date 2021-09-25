using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Common;

namespace WebSample.Entities
{
    public class Item : DataEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
    }
}
