using Repository.DTOs;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CustomerRepository
    {
        private LearningDbContext db;

        public CustomerRepository()
        {
            db = new LearningDbContext();
        }

        public List<Customer> GetAll()
        {
            return db.Customers.Include("Purchases").ToList();
        }

        public void Create(Customer cust)
        {
            db.Customers.Add(cust);
            db.SaveChanges();
        }

        public Customer Get(int custId)
        {
            return db.Customers.Where(c => c.Id == custId).FirstOrDefault();
        }

        public void Edit(Customer cust)
        {
            db.Entry(cust).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Customer toDelete)
        {
            db.Entry(toDelete).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }

        //DELETE THIS AFTER TESTING
        /*public Customer GetTopCustomer()
        {
            var topCustomer = db.Database.SqlQuery<CustomerDTO>("SELECT customerId, COUNT(*) AS 'sales' FROM Purchases GROUP BY customerId ORDER BY 'sales' DESC").FirstOrDefault();
            if (topCustomer != null)
            {
                Customer top = this.Get(topCustomer.customerId);
                return top;
            } else
            {
                return null;
            }
            
        }*/

        public TopCustomerDTO GetTopCustomerV2()
        {
            var topCustomer = db.Database.SqlQuery<TopCustomerDTO>("SELECT customerId, COUNT(*) AS 'sales', SUM(Price) AS 'salesValue' FROM Purchases INNER JOIN Motorcycles ON Purchases.motorcycleId = Motorcycles.Id GROUP BY customerId ORDER BY 'sales' DESC").FirstOrDefault();
            if (topCustomer != null)
            {
                Customer top = this.Get(topCustomer.CustomerId);
                topCustomer.Name = top.FirstName + " " + top.LastName;
                return topCustomer;
            }
            else
            {
                return null;
            }
        }
        
        public List<CustomerAndBikesDTO> CustomersAndBikes()
        {
            string sql = "SELECT Purchases.customerId, Customers.FirstName, Customers.LastName, " +
                "Motorcycles.Name FROM Purchases INNER JOIN Customers ON Purchases.customerId=Customers.Id " +
                "INNER JOIN Motorcycles ON Purchases.motorcycleId=Motorcycles.Id " +
                "WHERE Purchases.isActive = 'true'";
            var custAndBikes = db.Database.SqlQuery<CustomerAndBikesDTO>(sql).ToList(); //List of all customers with bike names purchased
            int flag; //Used to skip section of loop coming ahead
            List<CustomerAndBikesDTO> final = new List<CustomerAndBikesDTO>(); //List to have just customer and bikes, with bikes separated by ,


            for(int i = 0; i<custAndBikes.Count; i++)
            {
                //Reset flag
                flag = 1;
                //Add initial element always
                if (i == 0)
                {
                    final.Add(custAndBikes[i]);
                    continue;
                }

                //Loop to check if FINAL list already contains customer in CUSTANDBIKES list
                for(int j = 0; j <final.Count; j++)
                {
                    //If customer already in list, add bike name to that customer already existing bike
                    if(final[j].customerId == custAndBikes[i].customerId)
                    {
                        final[j].Name += ", " + custAndBikes[i].Name;
                        flag = 0;
                        break;
                    }
                }
                //Customer wasn't already in FINAL list, add it
                if (flag == 1)
                {
                    final.Add(custAndBikes[i]);
                }

            }

            var customers = this.GetAll();

            return final;

            
        }
        
    }
}
