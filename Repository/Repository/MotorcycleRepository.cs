using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class MotorcycleRepository
    {
        private LearningDbContext db;

        public MotorcycleRepository()
        {
            db = new LearningDbContext();
        }

        public List<Motorcycle> GetAll()
        {
            return db.Motorcycles.ToList();
        }

        public void Create(Motorcycle moto)
        {
            db.Motorcycles.Add(moto);
            db.SaveChanges();
        }

        public Motorcycle Get(int motoId)
        {
            return db.Motorcycles.Where(c => c.Id == motoId).FirstOrDefault();
        }

        public void Edit(Motorcycle moto)
        {
            db.Entry(moto).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Motorcycle toDelete)
        {
            db.Entry(toDelete).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }

        public  List<Motorcycle> GetAvailable()
        {
            //Get only purchases that are active. I assume if a purchase is not active, moto came back to inventory
            var pur = db.Purchases.Select(c => c).Where(c => c.isActive == true).ToList();
            List<int> custIds = new List<int>();

            //Get customer Ids from active purchases to check
            foreach(Purchase check in pur)
            {
                custIds.Add(check.motorcycleId);
            }

            var lista = db.Motorcycles.Where(c => !custIds.Contains(c.Id)).ToList();
            return lista;
        }

    }
}
