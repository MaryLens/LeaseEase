using leaseEase.BL;
using leaseEase.BL.Interfaces;
using leaseEase.BL.Repos;
using leaseEase.Domain.Models.Responces;
using leaseEase.Domain.Models.User;
using leaseEase.Web.Models.User;
using System;
using System.Web;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ISession _session;
        private readonly ILeaseEaseRepository _repo;
        public LoginController(ILeaseEaseRepository repo): base(repo)
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
            _repo = repo;
        }
        public ActionResult Index()
        {

            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new UActionLogin());
        }
        public ActionResult Signup()
        {

            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new UActionSignup());
        }
        [HttpPost]
        public ActionResult Signup(UActionSignup data)
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
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
                if (resp.Status)
                {
                    HttpCookie cookie = _session.CookieGenerate(urData.Email,_repo);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    currentSessionStatus();
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Signup", "Login");
            }
            return RedirectToAction("Signup", "Login");
        }

        [HttpPost]
        public ActionResult LogIn(UActionLogin data)
        {
            currentSessionStatus();
            var user = (leaseEase.Domain.Models.User.UserMinData)System.Web.HttpContext.Current.Session["SessionUser"];
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                var ulData = new UserLoginData
                {
                    Credential = data.Credential,
                    Password = data.Password,
                    LastLogin = DateTime.Now,
                    UserIp = base.Request.UserHostAddress
                };
                BaseResponces resp = _session.LoginUserActionFlow(ulData, _repo);
                if (resp.Status)
                {
                    HttpCookie cookie = _session.CookieGenerate(ulData.Credential, _repo);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    currentSessionStatus();
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
    }
}