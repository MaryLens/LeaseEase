using leaseEase.BL.Repos;
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
            public ActionResult BecomeCreator(){
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