﻿using leaseEase.Domain.Models.Off;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace leaseEase.Domain.Models.helpers
{
    public class newOfficeModel
    {
        public List<int> SelectedAmenityIds { get; set; }
        public List<Facility> Facilities { get; set; }
        public List<TypesOfOffice> TypesOfOffice { get; set; }
        public Office Office { get; set; }
        public List<HttpPostedFileBase> additionalImages {  get; set; }

    }
}
