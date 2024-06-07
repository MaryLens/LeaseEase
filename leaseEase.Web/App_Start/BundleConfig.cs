using System.Web.Optimization;

namespace leaseEase.Web.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-3.7.1.min.js"));
            //css
            bundles.Add(new StyleBundle("~/bundles/style1/css").Include(
                "~/Content/css/style.css", new CssRewriteUrlTransform()));

            //js
            bundles.Add(new ScriptBundle("~/bundles/app1/js").Include(
                "~/Scripts/js/app.js"));

        }
    }
}