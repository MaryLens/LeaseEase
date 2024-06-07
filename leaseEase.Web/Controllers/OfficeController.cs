using leaseEase.BL.Repos;
using leaseEase.Domain.Models.helpers;
using leaseEase.Domain.Models.Off;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class OfficeController : BaseController
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
            office.Views += 1;
            await _repo.UpdateOfficeAsync(office);
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
                SelectedAmenityIds = new List<int>(),
                Office = new Office()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> CreateNew(newOfficeModel model)
        {
            if (model.Office.ImageFile != null && model.Office.ImageFile.ContentLength > 0)
            {
                model.Office.Image = ConvertToBytes(model.Office.ImageFile);
            }
            if (model.SelectedAmenityIds != null)
            {
                foreach (int id in model.SelectedAmenityIds)
                {
                    model.Office.Facilities.Add(await _repo.GetFacilityByIdAsync(id));
                }
            }
            if(model.additionalImages != null)
            {
                foreach (var image in model.additionalImages)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        OfficeImg officeImage = new OfficeImg
                        {
                            Image = ConvertToBytes(image),
                            Office = model.Office
                        };
                        await _repo.AddOffImageAsync(officeImage);
                        model.Office.Images.Add(officeImage);
                    }
                }
            }
            await _repo.AddOfficeAsync(model.Office);
            return RedirectToAction("Details", new { id = model.Office.Id });
        }


        public async Task<ActionResult> LandlordDb()
        {
            return View(await _repo.GetAllOfficesAsync());
        }

        public async Task<ActionResult> Edit(int id)
        {
            var office = await _repo.GetOfficeByIdAsync(id);
            List<int> faci = new List<int>();
            var model = new newOfficeModel()
            {
                TypesOfOffice = await _repo.GetAllTypesAsync(),
                Facilities = await _repo.GetAllFacilitiessAsync(),
                Office = office,
                SelectedAmenityIds = faci
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(newOfficeModel model)
        {
            if (model.Office.ImageFile != null && model.Office.ImageFile.ContentLength > 0)
            {
                model.Office.Image = ConvertToBytes(model.Office.ImageFile);
            }

            model.Office.Facilities.Clear();
            if (model.SelectedAmenityIds != null)
            {
                foreach (int id in model.SelectedAmenityIds)
                {
                    var facility = await _repo.GetFacilityByIdAsync(id);
                    if (facility != null)
                    {
                        model.Office.Facilities.Add(facility);
                    }
                }
            }

            if (model.additionalImages != null)
            {
                foreach (var image in model.additionalImages)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        OfficeImg officeImage = new OfficeImg
                        {
                            Image = ConvertToBytes(image),
                            Office = model.Office
                        };
                        await _repo.AddOffImageAsync(officeImage);
                        model.Office.Images.Add(officeImage);
                    }
                }
            }

            await _repo.UpdateOfficeAsync(model.Office);
            return RedirectToAction("Details", new { id = model.Office.Id });
        }

        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] data = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                data = binaryReader.ReadBytes(file.ContentLength);
            }
            return data;
        }

        public async Task<ActionResult> NewBooking(int id)
        {
            Office office = await _repo.GetOfficeByIdAsync(id);
            newBookingViewModel model = new newBookingViewModel
            {
                Office = office,
                Booking = new Booking()

            };
            return office == null ? NotFound() : View(model);
        }

        [HttpPost]
        public async Task<ActionResult> NewBooking(newBookingViewModel model)
        {
            model.Booking.statusBooking = Domain.Enum.Off.statusBooking.NEW;
            await _repo.AddBookingAsync(model.Booking);
            return RedirectToAction("Details", new { id = model.Booking.OfficeId });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOff(int id)
        {
            var office = await _repo.GetOfficeByIdAsync(id);
            if (office != null)
            {
                await _repo.RemoveOfficeAsync(office.Id);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


    }
}