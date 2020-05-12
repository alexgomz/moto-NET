using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningEntityFramework.ViewModels
{
    public class PurchaseViewModel
    {

        public List<Customer> AvailCustomers { get; set; }
        public List<Motorcycle> AvailMotorcycles { get; set; }

        public Purchase Purchase { get; set; }



    }
}