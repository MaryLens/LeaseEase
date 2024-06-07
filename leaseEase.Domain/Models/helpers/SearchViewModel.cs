using leaseEase.Domain.Models.Off;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.helpers
{
    public class SearchViewModel
    {
        public List<Facility> Facilities { get; set; }
        public List<TypesOfOffice> TypesOfOffice { get; set; }
        public List<Office> Offices { get; set; }
    }
}
