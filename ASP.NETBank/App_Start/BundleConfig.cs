using System.Web.Optimization;

namespace ASP.NETBank
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-loader.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-messages.js",
                "~/Scripts/angular-touch.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-ui-ieshiv.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/ui-grid.js",
                "~/Scripts/toaster.min.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/angular-ui/ui-bootstrap",
                "~/Scripts/angular-ui.js",
                "~/Scripts/bootbox.js",
                ////  "~/Scripts/tr/angular.integralui.accordion.min.js",
                //"~/Scripts/tr/angular.integralui.min.js",
                //// "~/Scripts/tr/angular.integralui.checkbox.min.js",
                //"~/Scripts/tr/angular.integralui.lists.min.js",
                //// "~/Scripts/tr/angular.integralui.tooltip.min.js",
                ////"~/Scripts/tr/angular.integralui.treegrid.min.js",
                //"~/Scripts/tr/angular.integralui.treeview.min.js",
                //"~/Scripts/tr/theme.selector.min.js",
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/font-awesome.css",
                "~/Content/ui-grid.css",
                "~/Content/Site.css",
                "~/Content/css/themes/theme-flat-blue.css id='theme'",
                // "~/Content/css/integralui.treeview.css",
                // "~/Content/css/integralui.css",
                "~/Content/css/samples.css",
                "~/Content/font-awesome.css",
                "~/Content/ui-bootstrap-csp.css",
                "~/Content/toaster.min.css",
                "~/Content/bootstrap.css"));

            //minimization AngularJs Scripts
            const string angularAppRoot = "~/Angular/";
            const string virtualBundlePath = angularAppRoot + "main.js";

            var scriptBundle = new ScriptBundle(virtualBundlePath)
                .Include(angularAppRoot + "app.js")
                .IncludeDirectory(angularAppRoot, "*.js", searchSubdirectories: true);

            bundles.Add(scriptBundle);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
