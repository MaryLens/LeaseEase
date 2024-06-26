﻿using leaseEase.BL.Repos;
using leaseEase.Domain.Models.helpers;
using leaseEase.Domain.Models.Off;
using leaseEase.Domain.Models.User;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class OfficeController : BaseController
    {
        private readonly ILeaseEaseRepository _repo;
        public OfficeController(ILeaseEaseRepository repo) : base(repo)
        {
            _repo = repo;
        }

        // GET: Home
        public async Task<ActionResult> Details(int id)
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            Office office = await _repo.GetOfficeByIdAsync(id);
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser==null||office.Creator != currentUser.creatorData)
            {
                office.Views += 1;
            }
            if (currentUser!=null&&currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            await _repo.UpdateOfficeAsync(office);
            officeDetailsModel model = new officeDetailsModel {
                Office = office,
                Review = new Review()

        };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> NewReview(officeDetailsModel model)
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (model.Review.Rating < 1)
            {
                model.Review.Rating = 1;
            }
            if (model.Review.Rating > 5)
            {
                model.Review.Rating = 5;
            }
            model.Review.Office = await _repo.GetOfficeByIdAsync(model.Review.OfficeId);
            model.Office = model.Review.Office;
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (model.Review.Office.Creator == currentUser.creatorData)
            {
                return RedirectToAction("Details", new { id = model.Office.Id });
            } else if (!currentUser.MyBookings.Any(b => b.OfficeId == model.Review.Office.Id))
            {
                return RedirectToAction("Details", new { id = model.Office.Id });
            } else if (currentUser.MyReviews.Any(b => b.OfficeId == model.Review.Office.Id))
            {
                return RedirectToAction("Details", new { id = model.Office.Id });
            }
            model.Review.Creator = currentUser;
            model.Review.CreatorId = currentUser.Id;
            await _repo.AddReviewAsync(model.Review);
            return RedirectToAction("Details", new { id = model.Review.OfficeId });
        }

        public async Task<ActionResult> NotFound()
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            return View();
        }
        public async Task<ActionResult> CreateNew()
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (user.Role == Domain.Enum.User.Roles.Admin)
            {
                return RedirectToAction("AdminDb", "Admin");
            }
            else if (user.Role != Domain.Enum.User.Roles.Landlord)
            {
                return RedirectToAction("BecomeCreator", "User");
            }
            User currentUser = await _repo.GetUserByEmailAsync(user.Email);
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
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
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (user.Role == Domain.Enum.User.Roles.Admin)
            {
                return RedirectToAction("AdminDb", "Admin");
            }
            else if (user.Role != Domain.Enum.User.Roles.Landlord)
            {
                return RedirectToAction("BecomeCreator", "User");
            }
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
            model.Office.Creator = currentUser.creatorData;
            model.Office.CreatorId = currentUser.creatorData.Id;
            await _repo.AddOfficeAsync(model.Office);
            return RedirectToAction("Details", new { id = model.Office.Id });
        }


        public async Task<ActionResult> LandlordDb()
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            } else if (user.Role == Domain.Enum.User.Roles.Admin)
            {
                return RedirectToAction("AdminDb", "Admin");
            }
            else if (user.Role != Domain.Enum.User.Roles.Landlord)
            {
                return RedirectToAction("BecomeCreator", "User");
            }
            var currentUser = await _repo.GetUserByEmailAsync(user.Email);
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            var offices = await _repo.GetAllOfficesAsync();
            var bookings = await _repo.GetAllBookinsAsync();
            LandloredDBViewModel model = new LandloredDBViewModel {
            Offices = offices.Where(o=>o.Creator==currentUser.creatorData).ToList(),
            Bookings = bookings.Where(b=>b.Office.CreatorId==currentUser.creatorData.Id).ToList()
            };
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            currentSessionStatus();
            var office = await _repo.GetOfficeByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (office.Creator != currentUser.creatorData)
            {
                return RedirectToAction("Index", "Home");
            }
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
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            var oldOff = await _repo.GetOfficeByIdAsync(model.Office.Id);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (oldOff.Creator != currentUser.creatorData)
            {
                return RedirectToAction("Index", "Home");
            }
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
            oldOff.Name = model.Office.Name;
            oldOff.Description = model.Office.Description;
            oldOff.Type = model.Office.Type;
            oldOff.TypeId = model.Office.TypeId;
            oldOff.Price = model.Office.Price;
            oldOff.Location = model.Office.Location;
            oldOff.TeamSize = model.Office.TeamSize;
            oldOff.Image = model.Office.Image;
            oldOff.Rooms = model.Office.Rooms;
            oldOff.Size = model.Office.Size;
            oldOff.Facilities = model.Office.Facilities;
            await _repo.UpdateOfficeAsync(oldOff);
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
            currentSessionStatus();
            Office office = await _repo.GetOfficeByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (office.Creator == currentUser.creatorData)
            {
                return RedirectToAction("Details", new { id = id });
            }
            newBookingViewModel model = new newBookingViewModel
            {
                Office = office,
                Booking = new Booking()

            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> NewBooking(newBookingViewModel model)
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            model.Office = await _repo.GetOfficeByIdAsync(model.Booking.OfficeId);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (model.Office.Creator == currentUser.creatorData)
            {
                return RedirectToAction("Details", new { id = model.Office.Id });
            }
            model.Booking.statusBooking = Domain.Enum.Off.statusBooking.NEW;
            model.Booking.Creator = currentUser;
            model.Booking.CreatorId = currentUser.Id;
            await _repo.AddBookingAsync(model.Booking);
            return RedirectToAction("Details", new { id = model.Booking.OfficeId });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOff(int id)
        {
            currentSessionStatus();
            var office = await _repo.GetOfficeByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (office != null)
            {
                if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (office.Creator != currentUser.creatorData && user.Role!= Domain.Enum.User.Roles.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
                await _repo.RemoveOfficeAsync(office.Id);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<ActionResult> AcceptBooking(int id)
        {
            currentSessionStatus();
            var booking = await _repo.GetBookingByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (booking != null)
            {
                if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (booking.Office.CreatorId != currentUser.creatorData.Id)
            {
                return RedirectToAction("Index", "Home");
            }
                booking.statusBooking = Domain.Enum.Off.statusBooking.ACCEPTED;
                await _repo.UpdateBookingAsync(booking);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<ActionResult> DeclineBooking(int id)
        {
            currentSessionStatus();
            var booking = await _repo.GetBookingByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (booking != null)
            {
                if (user == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else if (booking.Office.CreatorId != currentUser.creatorData.Id)
                {
                    return RedirectToAction("Index", "Home");
                }
                booking.statusBooking = Domain.Enum.Off.statusBooking.DECLINED;
                await _repo.UpdateBookingAsync(booking);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<ActionResult> CancellBooking(int id)
        {
            currentSessionStatus();
            var booking = await _repo.GetBookingByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (booking != null)
            {
                if (user == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else if (booking.Creator != currentUser)
                {
                    return RedirectToAction("Index", "Home");
                }
                booking.statusBooking = Domain.Enum.Off.statusBooking.CANCELLED;
                await _repo.UpdateBookingAsync(booking);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<ActionResult> AddToFav(int id)
        {
            currentSessionStatus();
            var office = await _repo.GetOfficeByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (office != null)
            {
                if (user == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                currentUser.WishList.Add(office);
                await _repo.UpdateUserAsync(currentUser);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<ActionResult> StartChat(int id)
        {
            currentSessionStatus();
            var office = await _repo.GetOfficeByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (office != null)
            {
                if (user == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                var chat = new Chat();
                chat.Users.Add(currentUser);
                var anotherUser = await _repo.GetUserByIdAsync(office.Creator.UserId);
                chat.Users.Add(anotherUser);
                await _repo.AddNewChat(chat);
                await _repo.UpdateUserAsync(currentUser);
                return RedirectToAction("Chats", "User", new { chatId = chat.Id });
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<ActionResult> RemoveFromFav(int id)
        {
            currentSessionStatus();
            var office = await _repo.GetOfficeByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            User currentUser;
            if (user == null)
            {
                currentUser = null;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                currentUser = await _repo.GetUserByEmailAsync(user.Email);
            }
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            if (office != null)
            {
                if (user == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (currentUser.WishList.Contains(office))
                {
                    currentUser.WishList.Remove(office);
                    await _repo.UpdateUserAsync(currentUser);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }


    }
}