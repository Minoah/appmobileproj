using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Entities;
using WebSample.Common;
using WebSample.Repositories;

namespace WebSample.Services.MUserinfo
{
    public interface IUserinfoValidator : IServiceScoped
    {
        Task<bool> Login(Userinfo Userinfo);
        Task<bool> Create(Userinfo Userinfo);
        Task<bool> Update(Userinfo Userinfo);
        Task<bool> Delete(Userinfo Userinfo);
    }
    public class UserinfoValidator : IUserinfoValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            UserNameExisted,
            UserNameNotExisted,
            UserNameEmpty,
            UserNameOverLength,
            FirstNameEmpty,
            LastNameEmpty,
            PasswordEmpty,
            PasswordNotMatch
        }

        private IUOW UOW;
        public UserinfoValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<bool> ValidateId(Userinfo Userinfo)
        {
            UserinfoFilter UserinfoFilter = new UserinfoFilter
            {
                Skip = 0,
                Take = 10,
                UserId = new IdFilter { Equal = Userinfo.UserId },
                Selects = UserinfoSelect.UserId,
            };
            int count = await UOW.UserinfoRepository.Count(UserinfoFilter);
            if (count == 0)
                Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.UserId), ErrorCode.IdNotExisted);
            return Userinfo.IsValidated;
        }
        public async Task<bool> ValidateUserName(Userinfo Userinfo)
        {
            if (string.IsNullOrWhiteSpace(Userinfo.UserName))
                Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.UserName), ErrorCode.UserNameEmpty);
            else
            {
                
                if (Userinfo.UserName.Length > 255)
                {
               
                    Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.UserName), ErrorCode.UserNameOverLength);
                }
                UserinfoFilter UserinfoFilter = new UserinfoFilter
                {
                    Take = 10,
                    Skip = 0,
                    UserId = new IdFilter { NotEqual = Userinfo.UserId },
                    UserName = new StringFilter { Equal = Userinfo.UserName },
                    Selects = UserinfoSelect.UserName

                };
                int count = await UOW.UserinfoRepository.Count(UserinfoFilter);
                if (count != 0)
                    Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.UserName), ErrorCode.UserNameExisted);
            }

            return Userinfo.IsValidated;
        }
        public async Task<bool> ValidateFirstName(Userinfo Userinfo)
        {
            if (string.IsNullOrEmpty(Userinfo.FirstName))
                Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.FirstName), ErrorCode.FirstNameEmpty);
            return Userinfo.IsValidated;
        }
        public async Task<bool> ValidateLastName(Userinfo Userinfo)
        {
            if (string.IsNullOrEmpty(Userinfo.LastName))
                Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.LastName), ErrorCode.LastNameEmpty);
            return Userinfo.IsValidated;
        }
        public async Task<bool> ValidatePassword(Userinfo Userinfo)
        {
            if (string.IsNullOrEmpty(Userinfo.Password))
                Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.Password), ErrorCode.PasswordEmpty);
            return Userinfo.IsValidated;
        }
        public async Task<bool> Create(Userinfo Userinfo)
        {
            await ValidateUserName(Userinfo);
            await ValidateFirstName(Userinfo);
            await ValidateLastName(Userinfo);
            await ValidatePassword(Userinfo);
            return Userinfo.IsValidated;
        }

        public async Task<bool> Delete(Userinfo Userinfo)
        {
            await ValidateId(Userinfo);
            return Userinfo.IsValidated;
        }

        public async Task<bool> Update(Userinfo Userinfo)
        {
            await ValidateId(Userinfo);
            return Userinfo.IsValidated;
        }

        public async Task<bool> Login(Userinfo Userinfo)
        {
            if(string.IsNullOrEmpty(Userinfo.UserName) | string.IsNullOrEmpty(Userinfo.Password))
            {
                Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.UserName), ErrorCode.UserNameNotExisted);
            }
            else
            {
                UserinfoFilter UserinfoFilter = new UserinfoFilter
                {
                    Skip = 0,
                    Take = 1,
                    UserName = new StringFilter { Equal = Userinfo.UserName},
                    Selects = UserinfoSelect.ALL
                };
                List<Userinfo> Userinfos = await UOW.UserinfoRepository.List(UserinfoFilter);
                if(Userinfos.Count == 0)
                {
                    Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.UserName), ErrorCode.UserNameNotExisted);
                } else
                {
                    Userinfo userinfo = Userinfos.FirstOrDefault();
                    if(Userinfo.Password != userinfo.Password)
                        Userinfo.AddError(nameof(UserinfoValidator), nameof(Userinfo.UserName), ErrorCode.PasswordNotMatch);
                    else
                    {
                        Userinfo.UserId = userinfo.UserId;
                    }
                }
                
            }
            return Userinfo.IsValidated;
        }
    }
}
