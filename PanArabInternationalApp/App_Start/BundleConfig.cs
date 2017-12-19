using System.Web;
using System.Web.Optimization;

namespace PanArabInternationalApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/adminLite/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/adminLite/jquery-ui/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/adminLite/bootstrap/dist/js/bootstrap.min.js",
                        "~/adminLite/moment/min/moment.min.js",
                        "~/adminLite/bootstrap-daterangepicker/daterangepicker.js"
                        , "~/adminLite/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"
                        , "~/adminLite/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/adminLite/select2/dist/js/select2.full.min.js",
                        "~/adminLite/input-mask/jquery.inputmask.js",
                        "~/adminLite/input-mask/jquery.inputmask.date.extensions.js",
                        "~/adminLite/input-mask/jquery.inputmask.extensions.js",
                        "~/adminLite/fastclick/lib/fastclick.js",
                        "~/dist/js/adminlte.min.js",
                        "~/dist/js/pages/dashboard.js",
                        "~/adminLite/datatables.net/js/jquery.dataTables.min.js",
                         "~/adminLite/datatables.net-bs/js/dataTables.bootstrap.min.js",
"~/dist/js/demo.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/adminLite/bootstrap/dist/css/bootstrap.min.css",
                "~/adminLite/font-awesome/css/font-awesome.min.css",
                "~/adminLite/Ionicons/css/ionicons.min.css",
                "~/dist/css/AdminLTE.min.css",
                "~/dist/css/skins/_all-skins.min.css",
                "~/adminLite/morris.js/morris.css",
                "~/adminLite/jvectormap/jquery-jvectormap.css",
                "~/adminLite/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                "~/adminLite/bootstrap-daterangepicker/daterangepicker.css",
                "~/adminLite/select2/dist/css/select2.min.css"
               ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}