using leaseEase.BL.Repos;
using leaseEase.DAL;
using leaseEase.Web.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace leaseEase.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            leaseEaseContext context = new leaseEaseContext();
            ILeaseEaseRepository repo = new leaseEaseRepository(context);
            var controllerFactory = new DefaultControllerFactory(new CustomControllerActivator(repo));
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
