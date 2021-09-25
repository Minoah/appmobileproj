using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Entities;
using WebSample.Common;
using WebSample.Repositories;

namespace WebSample.Services.MUserinfo
{
    public interface IUserinfoService : IServiceScoped
    {
        Task<int> Count(UserinfoFilter UserinfoFilter);
        Task<List<Userinfo>> List(UserinfoFilter UserinfoFilter);
        Task<Userinfo> Get(long Id);
        Task<Userinfo> Create(Userinfo Userinfo);
        Task<Userinfo> Update(Userinfo Userinfo);
        Task<Userinfo> Login(Userinfo Userinfo);
        Task<Userinfo> RecoveryPassword(Userinfo Userinfo);
        UserinfoFilter ToFilter(UserinfoFilter UserinfoFilter);
    }
    public class UserinfoService : IUserinfoService
    {
        private IUOW UOW;
        private IUserinfoValidator UserinfoValidator;
        public UserinfoService(
            IUOW UOW,
            IUserinfoValidator UserinfoValidator
        )
        {
            this.UOW = UOW;
            this.UserinfoValidator = UserinfoValidator;
        }
        public async Task<int> Count(UserinfoFilter UserinfoFilter)
        {
            int result = await UOW.UserinfoRepository.Count(UserinfoFilter);
            return result;
        }

        public async Task<Userinfo> Create(Userinfo Userinfo)
        {
            if (!await UserinfoValidator.Create(Userinfo))
                return Userinfo;
            
            await UOW.UserinfoRepository.Create(Userinfo);
            Userinfo = await Get(Userinfo.UserId);
            return Userinfo;

        }

        public async Task<Userinfo> Get(long Id)
        {
            Userinfo Userinfo = await UOW.UserinfoRepository.Get(Id);
            return Userinfo;
        }

        public async Task<List<Userinfo>> List(UserinfoFilter UserinfoFilter)
        {
            List<Userinfo> Userinfos = await UOW.UserinfoRepository.List(UserinfoFilter);
            return Userinfos;
        }

        public async Task<Userinfo> Login(Userinfo Userinfo)
        {
            if (!await UserinfoValidator.Login(Userinfo))
                return Userinfo;
            UserinfoFilter UserinfoFilter = new UserinfoFilter
            {
                Take = 1,
                Skip = 0,
                UserName = new StringFilter { Equal = Userinfo.UserName },
                Password = new StringFilter { Equal = Userinfo.Password },
                Selects = UserinfoSelect.ALL,
            };
            List<Userinfo> Userinfos = await UOW.UserinfoRepository.List(UserinfoFilter);
            return Userinfos.FirstOrDefault();
        }

        public async Task<Userinfo> RecoveryPassword(Userinfo Userinfo)
        {
            throw new NotImplementedException();
        }

        public UserinfoFilter ToFilter(UserinfoFilter UserinfoFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<Userinfo> Update(Userinfo Userinfo)
        {
            if (!await UserinfoValidator.Update(Userinfo))
                return Userinfo;

            var oldData = await UOW.UserinfoRepository.Get(Userinfo.UserId);
            Userinfo.Password = oldData.Password;

            await UOW.UserinfoRepository.Update(Userinfo);
            Userinfo = await Get(Userinfo.UserId);
            return Userinfo;
        }
    }
}
