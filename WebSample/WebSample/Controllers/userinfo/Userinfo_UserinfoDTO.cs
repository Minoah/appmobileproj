using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Common;
using WebSample.Entities;

namespace WebSample.Controllers.userinfo
{
    public class Userinfo_UserinfoDTO : DataDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Userinfo_UserinfoDTO() { }
        public Userinfo_UserinfoDTO(Userinfo Userinfo)
        {
            this.UserId = Userinfo.UserId;
            this.UserName = Userinfo.UserName;
            this.FirstName = Userinfo.FirstName;
            this.LastName = Userinfo.LastName;
            this.Password = Userinfo.Password;
            this.Email = Userinfo.Email;
        }
    }

    public class Userinfo_UserinfoFilterDTO : FilterDTO
    {
        public IdFilter UserId { get; set; }
        public StringFilter FirstName { get; set; }
        public StringFilter LastName { get; set; }
        public StringFilter UserName { get; set; }
        public StringFilter Password { get; set; }
        public StringFilter Email { get; set; }
        public UserinfoOrder OrderBy { get; set; }
     }
}
