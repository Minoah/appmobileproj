using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Models;
using WebSample.Entities;
using WebSample.Common;
using Microsoft.EntityFrameworkCore;

namespace WebSample.Repositories
{
    public interface IUserinfoRepository
    {
        Task<int> Count(UserinfoFilter filter);
        Task<List<Userinfo>> List(UserinfoFilter filter);
        Task<Userinfo> Get(long Id);
        Task<bool> Create(Userinfo Userinfo);
        Task<bool> Update(Userinfo Userinfo);
        Task<bool> Delete(Userinfo Userinfo);
    }
    public class UserinfoRepository : IUserinfoRepository
    {
        private DataContext DataContext;
        public UserinfoRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }
        private IQueryable<UserinfoDAO> DynamicFilter(IQueryable<UserinfoDAO> query, UserinfoFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.UserId != null)
                query = query.Where(q => q.UserId, filter.UserId);
            if (filter.UserName != null)
                query = query.Where(q => q.UserName, filter.UserName);
            if (filter.Password != null)
                query = query.Where(q => q.Password, filter.Password);
            if (filter.FirstName != null)
                query = query.Where(q => q.FirstName, filter.FirstName);
            if (filter.LastName != null)
                query = query.Where(q => q.LastName, filter.LastName);
            if (filter.Email != null)
                query = query.Where(q => q.Email, filter.Email);

            return query;
        }
        private IQueryable<UserinfoDAO> DynamicOrder(IQueryable<UserinfoDAO> query, UserinfoFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case UserinfoOrder.UserId:
                            query = query.OrderBy(q => q.UserId);
                            break;
                        case UserinfoOrder.UserName:
                            query = query.OrderBy(q => q.UserName);
                            break;
                        case UserinfoOrder.FirstName:
                            query = query.OrderBy(q => q.FirstName);
                            break;
                        case UserinfoOrder.LastName:
                            query = query.OrderBy(q => q.LastName);
                            break;
                        case UserinfoOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case UserinfoOrder.UserId:
                            query = query.OrderByDescending(q => q.UserId);
                            break;
                        case UserinfoOrder.UserName:
                            query = query.OrderByDescending(q => q.UserName);
                            break;
                        case UserinfoOrder.FirstName:
                            query = query.OrderByDescending(q => q.FirstName);
                            break;
                        case UserinfoOrder.LastName:
                            query = query.OrderByDescending(q => q.LastName);
                            break;
                        case UserinfoOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;

                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }
        private async Task<List<Userinfo>> DynamicSelect(IQueryable<UserinfoDAO> query, UserinfoFilter filter)
        {
            List<Userinfo> Userinfos = await query.Select(q => new Userinfo()
            {
                UserId = filter.Selects.Contains(UserinfoSelect.UserId) ? q.UserId : default(int),
                FirstName = filter.Selects.Contains(UserinfoSelect.FirstName) ? q.FirstName : default(string),
                LastName = filter.Selects.Contains(UserinfoSelect.LastName) ? q.LastName : default(string),
                UserName = filter.Selects.Contains(UserinfoSelect.UserName) ? q.UserName : default(string),
                Email = filter.Selects.Contains(UserinfoSelect.Email) ? q.Email : default(string),
                Password = filter.Selects.Contains(UserinfoSelect.Password) ? q.Password : default(string),

            }).ToListAsync();
            return Userinfos;
        }
        public async Task<int> Count(UserinfoFilter filter)
        {
            IQueryable<UserinfoDAO> Userinfos = DataContext.Userinfos;
            Userinfos = DynamicFilter(Userinfos, filter);
            return await Userinfos.CountAsync();
        }
        public async Task<List<Userinfo>> List(UserinfoFilter filter)
        {
            if (filter == null) return new List<Userinfo>();
            IQueryable<UserinfoDAO> UserinfoDAOs = DataContext.Userinfos;
            UserinfoDAOs = DynamicFilter(UserinfoDAOs, filter);
            UserinfoDAOs = DynamicOrder(UserinfoDAOs, filter);
            List<Userinfo> Userinfos = await DynamicSelect(UserinfoDAOs, filter);
            return Userinfos;
        }
        public async Task<Userinfo> Get(long Id)
        {
            Userinfo Userinfo = await DataContext.Userinfos.AsNoTracking()
                .Where(x => x.UserId == Id)
                .Select(x => new Userinfo()
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    Email = x.Email,
                    Password = x.Password,
                    CreatedDate = x.CreatedDate
                }).FirstOrDefaultAsync();

            return Userinfo;
        }
        public async Task<bool> Create(Userinfo Userinfo)
        {
            UserinfoDAO UserinfoDAO = new UserinfoDAO();
            UserinfoDAO.UserId = Userinfo.UserId;
            UserinfoDAO.FirstName = Userinfo.FirstName;
            UserinfoDAO.LastName = Userinfo.LastName;
            UserinfoDAO.UserName = Userinfo.UserName;
            UserinfoDAO.Password = Userinfo.Password;
            UserinfoDAO.Email = Userinfo.Email;
            UserinfoDAO.CreatedDate = DateTime.Now;
            DataContext.Userinfos.Add(UserinfoDAO);
            await DataContext.SaveChangesAsync();
            Userinfo.UserId = UserinfoDAO.UserId;
            return true;
        }
        public async Task<bool> Update(Userinfo Userinfo)
        {
            UserinfoDAO UserinfoDAO = DataContext.Userinfos.Where(x => x.UserId == Userinfo.UserId).FirstOrDefault();
            if (UserinfoDAO == null)
                return false;

            UserinfoDAO.FirstName = Userinfo.FirstName;
            UserinfoDAO.LastName = Userinfo.LastName;
            UserinfoDAO.UserName = Userinfo.UserName;
            UserinfoDAO.Password = Userinfo.Password;
            UserinfoDAO.Email = Userinfo.Email;
            await DataContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Userinfo Userinfo)
        {
            await DataContext.Userinfos.Where(x => x.UserId == Userinfo.UserId).DeleteFromQueryAsync();
            return true;
        }
    }
}
