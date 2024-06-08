using leaseEase.BL.Repos;
using leaseEase.Domain.Models.Off;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult AdminDb()
        {
            return View();
        }
    }
}