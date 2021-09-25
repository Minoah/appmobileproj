using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Models;

namespace WebSample.Repositories
{
    public interface IUOW : IDisposable
    {

        Task Begin();
        Task Commit();
        Task Rollback();

        IProductRepository ProductRepository { get; }
        IItemRepository ItemRepository { get; }
        IUserinfoRepository UserinfoRepository { get; }
    }
    public class UOW : IUOW
    {
        private DataContext DataContext;
        public IProductRepository ProductRepository { get; private set; }
        public IItemRepository ItemRepository { get; private set; }
        public IUserinfoRepository UserinfoRepository { get; private set; }

        public UOW(DataContext DataContext)
        {
            this.DataContext = DataContext;
            ProductRepository = new ProductRepository(DataContext);
            ItemRepository = new ItemRepository(DataContext);
            UserinfoRepository = new UserinfoRepository(DataContext);
        }

        public async Task Begin()
        {
            return;
        }

        public Task Commit()
        {
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.DataContext == null)
            {
                return;
            }

            this.DataContext.Dispose();
            this.DataContext = null;
        }
    }
}
