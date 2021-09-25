using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Common;
using Newtonsoft.Json.Converters;

namespace WebSample.Entities
{
    public class Userinfo : DataEntity, IEquatable<Userinfo>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Equals(Userinfo other)
        {
            return other != null && UserId == other.UserId;
        }
        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }
        
    }
    public class UserinfoFilter : FilterEntity
    {
        public IdFilter UserId { get; set; }
        public StringFilter UserName { get; set; }
        public StringFilter Password { get; set; }
        public StringFilter FirstName { get; set; }
        public StringFilter LastName { get; set; }
        public StringFilter Email { get; set; }
        public UserinfoOrder OrderBy { get; set; }
        public UserinfoSelect Selects { get; set; }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserinfoOrder
    {
        UserId = 0,
        FirstName = 1,
        LastName = 2,
        UserName = 3,
        Email = 4
    }
    [Flags]
    public enum UserinfoSelect : long
    {
        ALL = E.ALL,
        UserId = E._0,
        FirstName = E._1,
        LastName = E._2,
        UserName = E._3,
        Email = E._4,
        Password = E._5,
    }
}
