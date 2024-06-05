using leaseEase.BL;
using leaseEase.BL.Interfaces;
using leaseEase.BL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace leaseEase.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISession _session;
        private readonly ILeaseEaseRepository _repo;
        public BaseController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }
        public void currentSessionStatus()
        {
            HttpCookie currentCookie = Request.Cookies["LEASEEASE"];
            if (currentCookie != null)
            {
                var profile = _session.GetUserByCookie(currentCookie.Value);
                if (profile != null)
                {
                    System.Web.HttpContext.Current.Session["SessionStatus"] = "isValid";
                    System.Web.HttpContext.Current.Session["SessionUser"] = profile;
                    return;
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("LEASEEASE"))
                    {
                        var cookie = ControllerContext.HttpContext.Request.Cookies["LEASEEASE"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }
                    System.Web.HttpContext.Current.Session["SessionStatus"] = "logout";
                }
            }
            System.Web.HttpContext.Current.Session["SessionStatus"] = "logout";
        }
    }
}