using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using WebStore.ViewModels.Orders;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB db;
        private readonly UserManager<User> userManager;

        public SqlOrderService(WebStoreDB db, UserManager<User> userManager) {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<Order> CreateOrderAsync(string Username, CartViewModel cart, OrderViewModel orderModel)
        {
            var user = await userManager.FindByNameAsync(Username);

            using(var transaction = await db.Database.BeginTransactionAsync())
            {
                var order = new Order
                {
                    Name = orderModel.Name,
                    Address = orderModel.Address,
                    Phone = orderModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };

                await db.AddAsync(order);

                foreach(var (productModel, quantity) in cart.Items)
                {
                    var product = await db.Products.FirstOrDefaultAsync(x => x.Id == productModel.Id);
                    if(product is null)
                        throw new InvalidOperationException($"Товар с Id: {productModel.Id} в базе данных не найден!");

                    var orderItem = new OrderItem
                    {
                        Order = order,
                        Product = product,
                        Price = product.Price,
                        Quantity = quantity
                    };

                    await db.OrderItems.AddAsync(orderItem);
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return order;
            }
        }

        public Order GetOrderById(int id) => db.Orders
            .Include(x => x.OrderItems)
            .FirstOrDefault(x => x.Id == id);

        public IEnumerable<Order> GetUserOrders(string Username) => db.Orders
            .Include(x => x.User)
            .Include(x => x.OrderItems)
            .Where(x => x.User.UserName == Username)
            .AsEnumerable();
    }
}
