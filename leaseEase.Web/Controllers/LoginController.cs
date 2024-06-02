using leaseEase.BL;
using leaseEase.BL.Interfaces;
using leaseEase.BL.Repos;
using leaseEase.Domain.Models.Responces;
using leaseEase.Domain.Models.User;
using leaseEase.Web.Models.User;
using System;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;
        private readonly ILeaseEaseRepository _repo;
        public LoginController(ILeaseEaseRepository repo)
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
            _repo = repo;
        }
        public ActionResult Index()
        {
            return View(new UActionLogin());
        }
        public ActionResult Signup()
        {
            return View(new UActionSignup());
        }
        [HttpPost]
        public ActionResult Signup(UActionSignup data)
        {
            var urData = new UserRegisterData
            {
                Name = data.Name,
                Email = data.Email,
                Password = data.Password,
                LastLogin = DateTime.Now,
                UserIp = base.Request.UserHostAddress
            };
            BaseResponces resp = _session.RegisterUserActionFlow(urData, _repo);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LogIn(UActionLogin data)
        {
            var ulData = new UserLoginData
            {
                Credential = data.Credential,
                Password = data.Password,
                LastLogin = DateTime.Now,
                UserIp = base.Request.UserHostAddress
            };
            BaseResponces resp = _session.ValidaeUserCredentialAction(ulData, _repo);
            if (resp.Status)
            {
                UCookieData cData = _session.GenCoockieAlgo(resp.CurrentUser);

                if (cData != null)
                {
                }
                BaseResponces auth = _session.GenerateUserSessionActionFlow(ulData, _repo);
            }
            return null;
        }
    }
}