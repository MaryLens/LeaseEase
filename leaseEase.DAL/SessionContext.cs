using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.DAL
{
    public class SessionContext : DbContext
    {
        public virtual DbSet<UDbSession> Sessions { get; set; }
        public SessionContext() : base("Host=localhost;Port=5432;Database=leaseEase;Username=postgres;Password=passForPGA")
        {
        }
    }
}
