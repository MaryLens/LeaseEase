using leaseEase.Domain.Models.Off;
using System.Collections.Generic;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class OfficeController : Controller
    {
        public List<Office> Offices { get; set; }
        // GET: Home
        public ActionResult Details(int id)
        {
            Office office = Offices.Find(p => p.Id == id);
            return office == null ? NotFound() : View(office);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}