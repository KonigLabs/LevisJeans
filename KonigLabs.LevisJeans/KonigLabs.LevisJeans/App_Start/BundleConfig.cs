using System.Web.Optimization;

namespace KonigLabs.LevisJeans
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.mCustomScrollbar.min.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/jquery.maskedinput.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/normalize.css",
                      "~/Content/style.css",
                      "~/Content/jquery.mCustomScrollbar.css",
                      "~/Content/toastr.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/toastr.min.js"));

            bundles.Add(new StyleBundle("~/Content/admin-css").Include(
                      "~/Content/normalize.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/toastr.min.css"));
        }
    }
}
