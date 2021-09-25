using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Models;
using WebSample.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebSample.Repositories
{
    public interface IItemRepository
    {
        Task<Item> Get(long Id);
        Task<bool> Create(Item Item);
        Task<bool> Update(Item Item);
        Task<bool> Delete(Item Item);
    }
    public class ItemRepository : IItemRepository
    {
        private DataContext DataContext;
        public ItemRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        public async Task<Item> Get(long Id)
        {
            Item Item = await DataContext.Items.AsNoTracking()
                .Where(x => x.Id == Id)
                .Select(x => new Item()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Code = x.Code,
                    Name = x.Name,
                    Product = x.Product == null ? null : new Product
                    {
                        ProductId = x.Product.ProductId,
                        Name = x.Product.Name,
                        Category = x.Product.Category,
                        Color = x.Product.Color,
                        UnitPrice = x.Product.UnitPrice,
                        AvailableQuantity = x.Product.AvailableQuantity,
                    }
                }).FirstOrDefaultAsync();

            return Item;
        }
        public async Task<bool> Create(Item Item)
        {
            ItemDAO ItemDAO = new ItemDAO();
            ItemDAO.Id = Item.Id;
            ItemDAO.ProductId = Item.ProductId;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            DataContext.Items.Add(ItemDAO);
            await DataContext.SaveChangesAsync();
            Item.Id = ItemDAO.Id;
            return true;
        }
        public async Task<bool> Update(Item Item)
        {
            ItemDAO ItemDAO = DataContext.Items.Where(x => x.Id == Item.Id).FirstOrDefault();
            if (ItemDAO == null)
                return false;
            ItemDAO.ProductId = Item.ProductId;
            ItemDAO.Code = Item.Code;
            ItemDAO.Name = Item.Name;
            await DataContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Item Item)
        {
            await DataContext.Items.Where(x => x.Id == Item.Id).DeleteFromQueryAsync();
            return true;
        }
    }
}
