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
        private IList<Shape> _shapes;

        public OrdersTests()
        {
            _orderRepository = new OrderRepository(new OrdersDbContext());
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, Order>());
            _shapes = new List<Shape>()
            {
                new Shape{Id = 1, Color = "Red"},
                new Shape{Id = 2, Color = "Black"},
                new Shape{Id = 3, Color = "Yellow"},
            };
        }

        [Fact]
        public async void Should_Group_Data_Successfully()
        {
            //Arrange
            //var dbOrder = new Order
            //{
            //    Description = "Test Order",
            //    OrderLines = new List<OrderLine>
            //    {
            //        new OrderLine {ProductId = 1, Quantity = 1}
            //    }
            //};
            //using (var dbContext = new OrdersDbContext())
            //{
            //    dbContext.Products.Add(new Product() {Name = "tEST"});
            //    dbContext.Orders.Add(dbOrder);
            //    await dbContext.SaveChangesAsync();
            //}

            //Act
            using (var dbContext = new OrdersDbContext())
            {
                var query = (from order in dbContext.Orders
                        join orderLine in dbContext.OrderLines on order.OrderId equals orderLine.OrderId into
                            nullableOrderLines
                        from orderLine in nullableOrderLines.DefaultIfEmpty()
                        join product in dbContext.Products on
                            orderLine.ProductId equals product.ProductId into nullableProducts
                        from product in nullableProducts.DefaultIfEmpty()
                        join shape in _shapes on product.ShapeId equals shape.Id 
                        select new OrderDto()
                        {
                            OrderId = order.OrderId,
                        }).OrderByDescending(v => v.Total)
                    .GroupBy(x => x.OrderId)
                    .SelectMany(x => x.Take(1));
                var result = await query.ToListAsync();

                
            }
        }
    }
}
