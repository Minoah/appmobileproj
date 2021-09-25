using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Common;
using WebSample.Entities;
using WebSample.Services.MUserinfo;

namespace WebSample.Controllers.userinfo
{
    public class UserinfoController : ControllerBase
    {
        private IUserinfoService UserinfoService;
        public UserinfoController(
            IUserinfoService UserinfoService
            )
        {
            this.UserinfoService = UserinfoService;
        }

        [Route("api/userinfo/count"), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Userinfo_UserinfoFilterDTO Userinfo_UserinfoFilterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            UserinfoFilter UserinfoFilter = ConvertFilterDTOToFilterEntity(Userinfo_UserinfoFilterDTO);
            int count = await UserinfoService.Count(UserinfoFilter);
            
            return count;
        }
        [Route("api/userinfo/register"), HttpPost]
        public async Task<ActionResult<Userinfo_UserinfoDTO>> Create([FromBody] Userinfo_UserinfoDTO Userinfo_UserinfoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            Userinfo Userinfo = ConvertDTOToEntity(Userinfo_UserinfoDTO);
            Userinfo = await UserinfoService.Create(Userinfo);
            Userinfo_UserinfoDTO = new Userinfo_UserinfoDTO(Userinfo);
            if (Userinfo.IsValidated)
                return Userinfo_UserinfoDTO;
            else
                return BadRequest(Userinfo_UserinfoDTO);
        }
        [Route("api/userinfo/login"), HttpPost]
        public async Task<ActionResult<Userinfo_UserinfoDTO>> Login([FromBody] Userinfo_UserinfoDTO Userinfo_UserinfoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            Userinfo Userinfo = ConvertDTOToEntity(Userinfo_UserinfoDTO);
            if (Userinfo == null)
                return BadRequest();
            Userinfo = await UserinfoService.Login(Userinfo);
            Userinfo_UserinfoDTO = new Userinfo_UserinfoDTO(Userinfo);
            if (Userinfo.IsValidated)
                return Ok(Userinfo_UserinfoDTO);
            else
                return BadRequest(Userinfo_UserinfoDTO);
        }
        private Userinfo ConvertDTOToEntity(Userinfo_UserinfoDTO Userinfo_UserinfoDTO)
        {
            Userinfo Userinfo = new Userinfo();
            Userinfo.UserId = Userinfo_UserinfoDTO.UserId;
            Userinfo.UserName = Userinfo_UserinfoDTO.UserName;
            Userinfo.Password = Userinfo_UserinfoDTO.Password;
            Userinfo.FirstName = Userinfo_UserinfoDTO.FirstName;
            Userinfo.LastName = Userinfo_UserinfoDTO.LastName;
            Userinfo.Email = Userinfo_UserinfoDTO.Email;
            return Userinfo;
        }

        private UserinfoFilter ConvertFilterDTOToFilterEntity(Userinfo_UserinfoFilterDTO Userinfo_UserinfoFilterDTO)
        {
            UserinfoFilter UserinfoFilter = new UserinfoFilter();
            UserinfoFilter.Selects = UserinfoSelect.ALL;
            UserinfoFilter.Skip = Userinfo_UserinfoFilterDTO.Skip;
            UserinfoFilter.Take = Userinfo_UserinfoFilterDTO.Take;
            UserinfoFilter.OrderBy = Userinfo_UserinfoFilterDTO.OrderBy;
            UserinfoFilter.OrderType = Userinfo_UserinfoFilterDTO.OrderType;

            UserinfoFilter.UserId = Userinfo_UserinfoFilterDTO.UserId;
            UserinfoFilter.UserName = Userinfo_UserinfoFilterDTO.UserName;
            UserinfoFilter.Password = Userinfo_UserinfoFilterDTO.Password;
            UserinfoFilter.FirstName = Userinfo_UserinfoFilterDTO.FirstName;
            UserinfoFilter.LastName = Userinfo_UserinfoFilterDTO.LastName;
            UserinfoFilter.Email = Userinfo_UserinfoFilterDTO.Email;
            return UserinfoFilter;
        }

    }
}
