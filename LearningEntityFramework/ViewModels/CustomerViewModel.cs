using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningEntityFramework.ViewModels
{
    public class CustomerViewModel
    {
        public List<Customer> AllCustomers { get; set; }
        public Customer NewCustomer { get; set; }
        public Customer EditCustomer { get; set; }
    }
}