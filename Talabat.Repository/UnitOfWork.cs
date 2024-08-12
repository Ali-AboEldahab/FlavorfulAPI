using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepository;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        public IGenericRepository<Product> ProductsRepo { get; set ; }
        public IGenericRepository<ProductBrand> BrandsRepo { get; set ; }
        public IGenericRepository<ProductCategory> CategoriesRepo { get; set ; }
        public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set ; }
        public IGenericRepository<OrderItem> OrderItemsRepo { get; set ; }
        public IGenericRepository<Order> OrdersRepo { get; set ; }

        public UnitOfWork(StoreContext context) //Ask CLR for Creating Object from DbContext
        {
            _context = context;
            ProductsRepo = new GenericRepository<Product>(_context);
            BrandsRepo = new GenericRepository<ProductBrand>(_context);
            CategoriesRepo = new GenericRepository<ProductCategory>(_context);
            DeliveryMethodsRepo = new GenericRepository<DeliveryMethod>(_context);
            OrderItemsRepo = new GenericRepository<OrderItem>(_context);
            OrdersRepo = new GenericRepository<Order>(_context);
        }
        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
