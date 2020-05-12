using Repository.DTOs;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningEntityFramework.Controllers
{
    public class ReportsController : Controller
    {
        private MotorcycleRepository repoBike;
        private CustomerRepository repoCustomer;
        private PurchaseRepository repoPurchase;

        public ReportsController()
        {
            repoBike = new MotorcycleRepository();
            repoCustomer = new CustomerRepository();
            repoPurchase = new PurchaseRepository();

        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopClient()
        {
            return View(repoCustomer.GetTopCustomerV2());
        }

        public ActionResult CustomerBikes()
        {
            List<CustomerAndBikesDTO> dtos = new List<CustomerAndBikesDTO>();
            var customers = repoCustomer.GetAll();

            foreach(var customer in customers)
            {
                CustomerAndBikesDTO dto = new CustomerAndBikesDTO();
                dto.customerId = customer.Id;
                dto.FirstName = customer.FirstName;
                dto.LastName = customer.LastName;
                dto.Name = string.Join(", ", customer.Purchases.Select(p => p.Motorcycle.Name).ToList());

                dtos.Add(dto);
            }


            return View(dtos);
        }
    }
}