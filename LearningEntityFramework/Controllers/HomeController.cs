using LearningEntityFramework.ViewModels;
using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LearningEntityFramework.Controllers
{
    public class HomeController : Controller
    {

        private MotorcycleRepository repoBike;
        private CustomerRepository repoCustomer;
        private PurchaseRepository repoPurchase;

        public HomeController()
        {
            repoBike = new MotorcycleRepository();
            repoCustomer = new CustomerRepository();
            repoPurchase = new PurchaseRepository();
        }

        public ActionResult Index()
        {
            return View();
        }
        
        //Motorcycle Pages Here ------------------------------------------------------------------------------
        public ActionResult Motorcycles()
        {
            MotorcycleViewModel motos = new MotorcycleViewModel();
            motos.AllMotorcycles = repoBike.GetAll();
            return View(motos);
        }

        [HttpPost]
        public ActionResult Motorcycles(MotorcycleViewModel motos)
        {
            if (motos.EditMotorcycle != null)
            {
                repoBike.Edit(motos.EditMotorcycle);
            }
            else if (motos.NewMotorcycle != null)
            {
                repoBike.Create(motos.NewMotorcycle);
            }

            return RedirectToAction("Motorcycles");
        }
        
        public ActionResult NewMotorcycleForm()
        {
            return PartialView("../Partial/NewMotorcycleForm");
        }

        public ActionResult EditMotorcycleForm(string id)
        {
            int motoIdInt;
            int.TryParse(id, out motoIdInt);
            MotorcycleViewModel edit = new MotorcycleViewModel();
            edit.EditMotorcycle = repoBike.Get(motoIdInt);
            if (edit.EditMotorcycle == null) return View("ErrorPage");


            return PartialView("../Partial/EditMotorcycleForm", edit);
        }


        public ActionResult DeleteMotorcycle(int motoId)
        {
            Motorcycle toDelete = repoBike.Get(motoId);
            return View(toDelete);
        }

        [HttpPost]
        public ActionResult DeleteMotorcycle(Motorcycle moto)
        {
            repoBike.Delete(moto);
            return RedirectToAction("Motorcycles");
        }

        //Customer Pages Here ------------------------------------------------------------------------------
        public ActionResult Customers()
        {
            CustomerViewModel custs = new CustomerViewModel();
            custs.AllCustomers = repoCustomer.GetAll();
            return View(custs);
        }

        [HttpPost]
        public string Customers(Customer custs)
        {
            if(custs.Id > 0)
            {
                repoCustomer.Edit(custs);
            } else
            {
                repoCustomer.Create(custs);
            }

            return "200";

        }

        public string GetCustomer(string id)
        {
            int custId;
            int.TryParse(id, out custId);
            Customer found = repoCustomer.Get(custId);
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(found);
        }

        public string GetData()
        {
            var list = repoCustomer.GetAll().Select(c=> new { c.Id, c.FirstName, c.LastName});

            var jsonSerializer = new JavaScriptSerializer();

            return jsonSerializer.Serialize(list);
        }


        public ActionResult EditCustomerForm(string id)
        {
            int realId;
            int.TryParse(id, out realId);
            CustomerViewModel edit = new CustomerViewModel();
            edit.EditCustomer = repoCustomer.Get(realId);
            if (edit.EditCustomer == null) return RedirectToAction("ErrorPage");

            return PartialView("../Partial/EditCustomerForm", edit);
        }
        

        public ActionResult DeleteCustomer(int custId)
        {
            Customer toDelete = repoCustomer.Get(custId);
            if (toDelete == null) return View("ErrorPage");

            return View(toDelete);
           
        }

        [HttpPost]
        public ActionResult DeleteCustomer(Customer toDelete)
        {
            repoCustomer.Delete(toDelete);
            return RedirectToAction("Customers");
        }
        

        //Purchase Pages Here ----------------------------------------------------------------------------------
        public ActionResult Purchases()
        {
            return View(repoPurchase.GetAll());
        }

        public ActionResult CreatePurchase()
        {

            PurchaseViewModel purchase = new PurchaseViewModel();
            purchase.AvailCustomers = repoCustomer.GetAll();
            purchase.AvailMotorcycles = repoBike.GetAvailable();
            purchase.Purchase = new Purchase();

            return View(purchase);
        }

        [HttpPost]
        public ActionResult CreatePurchase(PurchaseViewModel pur)
        {

            repoPurchase.Create(pur.Purchase);
            return RedirectToAction("Purchases");
        }


        public ActionResult DeletePurchase(int purId)
        {
            Purchase toDelete = repoPurchase.Get(purId);
            if (toDelete == null) return View("ErrorPage");

            return View(toDelete);
        }

        [HttpPost]
        public ActionResult DeletePurchase(Purchase toDelete)
        {
            repoPurchase.Delete(toDelete);

            return RedirectToAction("Purchases");
        }

    }
}