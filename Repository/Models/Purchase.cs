using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Purchase
    {
        public Purchase()
        {
            PurchaseDate = DateTime.Now;
            isActive = true;
        }

        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int motorcycleId { get; set; }
        public int customerId { get; set; }

        public bool isActive { get; set; }

        public virtual Motorcycle Motorcycle { get; set; }
        public virtual Customer customer { get; set; }
    }
}
