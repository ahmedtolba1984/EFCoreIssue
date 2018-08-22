using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreIssue.Models;

namespace EFCoreIssue
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new OrdersDbContext())
            {
                var order = new Order { Description = "First Order"};
                db.Orders.Add(order);
                db.SaveChanges();
            }


            using (var db = new OrdersDbContext())
            {
                var orders = db.Orders.ToList();
                Console.WriteLine(orders.Count);
            }
        }
    }
}
