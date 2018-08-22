using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreIssue.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int Total { get; set; }
        public string Description { get; set; }
        public IList<OrderLineDto> OrderLines { get; set; }
    }
}
