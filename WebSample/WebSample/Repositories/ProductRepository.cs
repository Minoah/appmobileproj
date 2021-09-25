using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSample.Models;
using WebSample.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebSample.Repositories
{
    public interface IProductRepository { }
    public class ProductRepository:IProductRepository
    {
        private DataContext DataContext;
        public ProductRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }
    }
}
