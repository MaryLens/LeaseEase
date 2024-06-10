using leaseEase.BL.Repos;
using System.Threading.Tasks;
using leaseEase.Domain.Models.Off;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using leaseEase.Domain.Models.helpers;
using leaseEase.Domain.Models.User;

namespace leaseEase.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ILeaseEaseRepository _repo;
        public AdminController(ILeaseEaseRepository repo) : base(repo)
        {
            _repo = repo;
        }
        // GET: Admin
        public async Task<ActionResult> AdminDb()
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (user.Role != Domain.Enum.User.Roles.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
                var currentUser = await _repo.GetUserByEmailAsync(user.Email);
                if (currentUser.Blocked)
                {
                    return RedirectToAction("Blocked", "User");
                }
            var offices = await _repo.GetAllOfficesAsync();
            var users = await _repo.GetAllUsersAsync();
            AdminDBViewModel model = new AdminDBViewModel
            {
                Offices = offices,
                Users = users.Where(u=>u!=currentUser).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> BlockUser(int id)
        {
            currentSessionStatus();
            var userToBlock = await _repo.GetUserByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user.Role!= Domain.Enum.User.Roles.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            if (userToBlock != null)
            {
                userToBlock.Blocked = true;
                await _repo.UpdateUserAsync(userToBlock);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<ActionResult> UnblockUser(int id)
        {
            currentSessionStatus();
            var userToBlock = await _repo.GetUserByIdAsync(id);
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user.Role != Domain.Enum.User.Roles.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            if (userToBlock != null)
            {
                userToBlock.Blocked = false;
                await _repo.UpdateUserAsync(userToBlock);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}