using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs
{
    public class TopCustomerDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int Sales { get; set; }
        public decimal SalesValue { get; set; }
        
    }
}
