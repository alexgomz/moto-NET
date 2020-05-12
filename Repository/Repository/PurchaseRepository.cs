using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PurchaseRepository
    {
        private LearningDbContext db;

        public PurchaseRepository()
        {
            db = new LearningDbContext();
        }

        public List<Purchase> GetAll()
        {
            return db.Purchases.Where(c => c.isActive == true).ToList();
        }
        public Purchase Get(int purId)
        {
            return db.Purchases.Where(c => c.Id == purId).FirstOrDefault();
        }


        public void Create(Purchase pur)
        {
            db.Purchases.Add(pur);
            db.SaveChanges();
        }

        public void Edit(Purchase pur)
        {
            db.Entry(pur).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Purchase toDelete)
        {
            //db.Entry(toDelete).State = System.Data.Entity.EntityState.Deleted;
            db.Entry(toDelete).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
