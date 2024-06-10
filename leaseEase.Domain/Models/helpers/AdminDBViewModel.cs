using leaseEase.Domain.Models.Off;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.helpers
{
   public class AdminDBViewModel
    {
        public List<Office> Offices { get; set; }
        public List<leaseEase.Domain.Models.User.User> Users { get; set; }
    }
}
