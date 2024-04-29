using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using leaseEase.DAL;
using leaseEase.Domain.Models.Off;
using leaseEase.Domain.Models.helpers;
using leaseEase.BL.Repos;
using System.Threading.Tasks;


namespace leaseEase.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILeaseEaseRepository _repo;
        public HomeController(ILeaseEaseRepository repo)
        {
                _repo = repo;
        }
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var model = new IndexViewModel
            {
                TypesOfOffice = await _repo.GetAllTypesAsync(),
                Facilities = await _repo.GetAllFacilitiessAsync()
            };

            return View(model);
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}