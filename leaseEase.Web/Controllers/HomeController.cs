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
    public class HomeController : BaseController
    {
        private readonly ILeaseEaseRepository _repo;

        public HomeController(ILeaseEaseRepository repo)
        {
                _repo = repo;
        }
        // GET: Home
        public async Task<ActionResult> Index()
        {
            List <Office> office = await _repo.GetAllOfficesAsync();
            List<Office> finalOffice = office.OrderByDescending(o => o.Views).Take(4).ToList();
            var model = new IndexViewModel
            {
                TypesOfOffice = await _repo.GetAllTypesAsync(),
                Facilities = await _repo.GetAllFacilitiessAsync(),
                Office = finalOffice
            };

            return View(model);
        }

        public async Task<ActionResult> Search()
        {
            return View(await _repo.GetAllOfficesAsync());
        }
    }
}