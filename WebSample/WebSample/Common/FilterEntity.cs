using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebSample.Common
{
    public class FilterEntity
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public OrderType OrderType { get; set; }

        public FilterEntity()
        {
            Skip = 0;
            Take = 10;
            OrderType = OrderType.DESC;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType
    {
        DESC,
        ASC,
    }
}
