
using leaseEase.Domain.Models.Off;
using System;
using System.Linq;
using leaseEase.DAL;

namespace leaseEase.BL.DBModel.Off
{
    public class OfficeService
    {
        public Office Add(string name, string description, leaseEaseContext db)
        {
            using(db)
            {
                Office office = new Office {Name = name, Description = description };
                db.Offices.Add(office);
                db.SaveChanges();
                return office;
            }
        }

        public Office Get(int id, leaseEaseContext db)
        {
            using (db)
            {
                var office = db.Offices.FirstOrDefault(o => o.Id == id);
                return office;
            }
        }

        public Office Delete(int id, leaseEaseContext db)
        {
            using (db)
            {
                var office = db.Offices.FirstOrDefault(o => o.Id == id);
                if(office == null)
                {
                    db.Offices.Remove(office);
                    db.SaveChanges();
                }
                return null;
            }
        }

    }
}
