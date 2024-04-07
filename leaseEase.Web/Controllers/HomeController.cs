using System.Collections.Generic;
using System.Web.Mvc;
using leaseEase.BL.DBModel.Off;
using leaseEase.DAL;
using leaseEase.Domain.Models.Off;


namespace leaseEase.Web.Controllers
{
    public class HomeController : Controller
    {

        public List<Office> Offices { get; set; }
        public HomeController()
        {
            using (leaseEaseContext db = new leaseEaseContext())
            {
                OfficeService os = new OfficeService();
                Office office = os.Add("o1","some description",db);
            }
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}