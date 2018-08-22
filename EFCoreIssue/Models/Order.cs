using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreIssue.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Total { get; set; }
        public string Description { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
    }
}
