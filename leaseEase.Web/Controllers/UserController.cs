using leaseEase.BL.Repos;
using leaseEase.Domain.Models.helpers;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILeaseEaseRepository _repo;
        public UserController(ILeaseEaseRepository repo) : base(repo)
        {
            _repo = repo;
        }
            // GET: User
            public async Task<ActionResult> BecomeCreator(){
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
            else if (user.Role == Domain.Enum.User.Roles.Landlord)
            {
                return RedirectToAction("LandlordDb", "Office");
            }
            var currentUser = await _repo.GetUserByEmailAsync(user.Email);
            if (currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            return View(new TobeCreatorData());
        }
        [HttpPost]
        public async Task<ActionResult> BecomeCreator(TobeCreatorData creatorData)
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
            else if (user.Role == Domain.Enum.User.Roles.Landlord)
            {
                return RedirectToAction("LandlordDb", "Office");
            }

            var currentUser = await _repo.GetUserByEmailAsync(user.Email);
            if (currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            currentUser.Role = Domain.Enum.User.Roles.Landlord;

            if (creatorData.ImageFile != null && creatorData.ImageFile.ContentLength > 0)
            {
                creatorData.Image = ConvertToBytes(creatorData.ImageFile);
            }

            creatorData.User = currentUser;
            creatorData.UserId = currentUser.Id;
            currentUser.creatorData = creatorData;

            await _repo.UpdateUserAsync(currentUser);

            currentSessionStatus();

            return RedirectToAction("Index", "Home");
        }
        public async Task<ActionResult> Blocked()
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
                if (currentUser == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (!currentUser.Blocked)
                {
                    return RedirectToAction("Index", "Home");
                }
            return View(); 
        }
        public async Task<ActionResult> MyBookings()
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = await _repo.GetUserByEmailAsync(user.Email);
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            var bookings = await _repo.GetAllBookinsAsync();
            MyBookingsViewModel model = new MyBookingsViewModel
            {
                Bookings = bookings.Where(b => b.Creator == currentUser).ToList()
            };
            return View(model);
        }
        public async Task<ActionResult> Favourites()
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var currentUser = await _repo.GetUserByEmailAsync(user.Email);
            if (currentUser != null && currentUser.Blocked)
            {
                return RedirectToAction("Blocked", "User");
            }
            FavouritesViewModel model = new FavouritesViewModel
            {
                Offices = currentUser.WishList
            };
            return View(model);
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
    }
}