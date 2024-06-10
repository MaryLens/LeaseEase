using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using leaseEase.Domain.Models.Off;
using leaseEase.Domain.Models.helpers;
using leaseEase.BL.Repos;
using System.Threading.Tasks;
using System.Globalization;
using leaseEase.Domain.Models.User;


namespace leaseEase.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILeaseEaseRepository _repo;

        public HomeController(ILeaseEaseRepository repo) : base(repo)
        {
                _repo = repo;
        }
        // GET: Home
        public async Task<ActionResult> Index()
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user != null)
            {
                var currentUser = await _repo.GetUserByEmailAsync(user.Email);
                if (currentUser.Blocked)
                {
                    return RedirectToAction("Blocked", "User");
                }
            }
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

        public async Task<ActionResult> Search(string locationFilter, decimal? priceFilter, string sortFilter, List<int> typeFilters, List<int> faciFilters)
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user != null)
            {
                var currentUser = await _repo.GetUserByEmailAsync(user.Email);
                if (currentUser.Blocked)
                {
                    return RedirectToAction("Blocked", "User");
                }
            }
            var model = new SearchViewModel
            {
                Offices = await _repo.GetAllOfficesAsync(),
                TypesOfOffice = await _repo.GetAllTypesAsync(),
                Facilities = await _repo.GetAllFacilitiessAsync()
            };

            if (!string.IsNullOrEmpty(locationFilter))
            {
                string lowerLocationFilter = locationFilter.ToLower();
                model.Offices = model.Offices.Where(o => o.Location.ToLower().Contains(lowerLocationFilter)).ToList();
            }

            if (priceFilter !=0 && priceFilter!=null)
            {
                model.Offices = model.Offices.Where(o => o.Price<=priceFilter).ToList();
            }
            if (typeFilters != null && typeFilters.Any())
            {
                model.Offices = model.Offices.Where(o => typeFilters.Contains(o.Type.Id)).ToList();
            }

            if (faciFilters != null && faciFilters.Any())
            {
                model.Offices = model.Offices
                .Where(o => faciFilters.All(f => o.Facilities.Any(of => of.Id == f)))
                .ToList();
            }
            if (!string.IsNullOrEmpty(sortFilter))
            {
                switch (sortFilter)
                {
                    case "Nosort":
                        model.Offices = model.Offices.ToList();
                        break;
                    case "Popular":
                        model.Offices = model.Offices.OrderByDescending(o => o.Views).ToList();
                        break;
                    case "Rating":
                        model.Offices = model.Offices.OrderByDescending(o => o.Rating).ToList();
                        break;
                    case "NameAsc":
                        model.Offices = model.Offices.OrderBy(o => o.Name).ToList();
                        break;
                    case "NameDesc":
                        model.Offices = model.Offices.OrderByDescending(o => o.Name).ToList();
                        break;
                    case "PriceAsc":
                        model.Offices = model.Offices.OrderBy(o => o.Price).ToList();
                        break;
                    case "PriceDesc":
                        model.Offices = model.Offices.OrderByDescending(o => o.Price).ToList();
                        break;
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OfficeList", model);
            }

            return View(model);
        }


    }
}