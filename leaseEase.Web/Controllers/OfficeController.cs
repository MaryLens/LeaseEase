using leaseEase.BL.Repos;
using leaseEase.Domain.Models.helpers;
using leaseEase.Domain.Models.Off;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class OfficeController : Controller
    {
        private readonly ILeaseEaseRepository _repo;
        public List<Office> Offices { get; set; }
        public OfficeController(ILeaseEaseRepository repo) {
            _repo = repo;
        }

        // GET: Home
        public async Task<ActionResult> Details(int id)
        {
            Office office = await _repo.GetOfficeByIdAsync(id);
            officeDetailsModel model = new officeDetailsModel {
                Office = office,
                Review = new Review()

        };
            return office == null ? NotFound() : View(model);
        }

        [HttpPost]
        public async Task<ActionResult> NewReview(officeDetailsModel model)
        {
            await _repo.AddReviewAsync(model.Review);
            return RedirectToAction("Details", new { id = model.Review.OfficeId });
        }

        public ActionResult NotFound()
        {
            return View();
        }
        public async Task<ActionResult> CreateNew()
        {
            var model = new newOfficeModel
            {
                TypesOfOffice = await _repo.GetAllTypesAsync(),
                Facilities = await _repo.GetAllFacilitiessAsync(),
                Office = new Office()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> CreateNew(newOfficeModel model)
        {
            if (model.Office.ImageFile != null && model.Office.ImageFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(model.Office.ImageFile.InputStream))
                {
                    byte[] fileData = binaryReader.ReadBytes(model.Office.ImageFile.ContentLength);
                    model.Office.Image = fileData;
                }
            }
            await _repo.AddOfficeAsync(model.Office);
            return RedirectToAction("Details", new { id = model.Office.Id });
        }
    }
}