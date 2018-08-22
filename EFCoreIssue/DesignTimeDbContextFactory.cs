//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace EFCoreIssue
//{
//    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<OrdersDbContext>
//    {
//        public OrdersDbContext CreateDbContext(string[] args)
//        {
//            var builder = new DbContextOptionsBuilder<OrdersDbContext>();
//            builder.UseSqlServer("Server=localhost; Database=EFCoreIssue; Trusted_Connection=True;");

//            return new OrdersDbContext(builder.Options);
//        }
//    }
//}
