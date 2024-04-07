using System.Web.Optimization;

namespace leaseEase.Web.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            //css
            bundles.Add(new StyleBundle("~/bundles/style1/css").Include(
                "~/Content/css/style.css", new CssRewriteUrlTransform()));

            //js
            bundles.Add(new ScriptBundle("~/bundles/app1/js").Include(
                "~/Scripts/js/app.js"));

        }
    }
}