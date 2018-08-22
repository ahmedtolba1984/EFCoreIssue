using System;
using System.Linq;
using System.Threading.Tasks;
using EFCoreIssue.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIssue
{
    public class OrderRepository
    {
        private readonly OrdersDbContext _dbContext;
        private DbSet<Order> Table => _dbContext.Set<Order>();

        public OrderRepository(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var dbOrder = await GetOrderAsync(order.OrderId);
            if (dbOrder == null)
                throw new Exception($"Invalid Or Missing Order with Id {order.OrderId}");

            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            var order = await _dbContext.Orders.Include(x => x.OrderLines).FirstOrDefaultAsync(x => x.OrderId == orderId);
            return order;
        }

        public Task<Order> InsertAsync(Order order)
        {
            var entity = Table.Add(order).Entity;
            return Task.FromResult(entity);
        }

    }
}
