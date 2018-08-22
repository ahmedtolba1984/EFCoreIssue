using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EFCoreIssue;
using EFCoreIssue.Dtos;
using EFCoreIssue.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFCoreIssue_Tests
{
    public class OrdersTests
    {
        private readonly OrderRepository _orderRepository;

        public OrdersTests()
        {
            _orderRepository = new OrderRepository(new OrdersDbContext());
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, Order>());
        }

        [Fact]
        public async void Should_Update_Customer_Successfully()
        {
            //Arrange
            var order = new Order
            {
                Description = "Test Order",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine {ProductId = 1, Quantity = 1}
                }
            };
            using (var dbContext = new OrdersDbContext())
            {
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();
            }

            //Act
            var orderDto = new OrderDto
            {
                Description = "Test Order 2",
                OrderLines = new List<OrderLineDto>
                {
                    new OrderLineDto {OrderLineId = order.OrderLines[0].OrderLineId, ProductId = 2, Quantity = 2}
                },
                OrderId = order.OrderId
            };


            await _orderRepository.UpdateAsync(Mapper.Map<Order>(orderDto));

            //Assert
            using (var dbContext = new OrdersDbContext())
            {
                var updatedOrder = await dbContext.Orders.Include(c => c.OrderLines)
                    .Where(c => c.OrderId == order.OrderId).SingleAsync();
                //updatedOrder.ShouldNotBeNull();
                //customerFetch.Id.ShouldNotBeNull();
                //customerFetch.Id.ShouldBe(customerDto.Id);
            }
        }
    }
}
