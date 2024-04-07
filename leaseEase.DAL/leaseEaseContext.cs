using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using leaseEase.Domain.Models.Off;

namespace leaseEase.DAL
{
    public class leaseEaseContext : DbContext
    {

        public DbSet<Office> Offices { get; set; }
        public leaseEaseContext() : base("Host=localhost;Port=5432;Database=leaseEase;Username=postgres;Password=passForPGA")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<leaseEaseContext>());
        }

    }
}
