using System.Collections.Generic;

namespace WebSample.Common
{
    public class DataDTO
    {
        public Dictionary<string, string> Errors;
    }

    public class FilterDTO
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public OrderType OrderType { get; set; }

        public FilterDTO()
        {
            Skip = 0;
            Take = 10;
            OrderType = OrderType.DESC;
        }
    }

    /*public class AccessRight
    {
        public List<string> Actions { get; set; }
        public List<FilterPermissionDefinition> Filters { get; set; }
    }*/
}
