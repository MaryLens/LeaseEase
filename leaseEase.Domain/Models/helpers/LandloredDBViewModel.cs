using leaseEase.Domain.Models.Off;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.helpers
{
    public class LandloredDBViewModel
    {
        public List<Office> Offices { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
