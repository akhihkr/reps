using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace REPS.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.ResetAll();
            bundles.Add(new ScriptBundle("~/Bundles/Scripts").Include(
                      //"~/Javascripts/jquery.min.js",
                      "~/Javascripts/vendor/foundation.min.js",
                      "~/Javascripts/app.js",
                      "~/Javascripts/Chart.min.js",
                      "~/Javascripts/vendor/what-input.js",
                      "~/Javascripts/vendor/jquery.unobtrusive-ajax.min.js",
                      "~/Javascripts/toastr.min.js", //toaster for popup notification
                      "~/Javascripts/Datedropper3-master/datedropper.min.js",
                      "~/Javascripts/timedropper-master/timedropper.min.js",
                      "~/Javascripts/jquery-sortable.js",
                      "~/Javascripts/paging.js"));
                      //"~/Javascripts/dirrty.js"));

            bundles.Add(new ScriptBundle("~/Bundles/Correspondence").Include(
                      "~/Javascripts/tinymce/jquery.tinymce.min.js",
                      "~/Javascripts/tinymce/tinymce.min.js"));

            bundles.Add(new ScriptBundle("~/Bundles/Timeline").Include(                 
                      "~/Javascripts/Timeline.js"));

            bundles.Add(new ScriptBundle("~/Bundles/jqueryui").Include(
                       "~/Javascripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/Bundles/JqueryMin").Include(
                        "~/Javascripts/jquery.min.js"));

            //bundles.Add(new ScriptBundle("~/Bundles/Charts").Include(
            //          "~/Javascripts/Chart.min.js"));

            bundles.Add(new ScriptBundle("~/Bundles/MultiSelect").Include(
                    "~/Javascripts/jquery.multi-select.js")); //  for admin workflow left right selection 


            bundles.Add(new ScriptBundle("~/Bundles/JSAPI").Include(
                      "~/Javascripts/jsapi.js"
                     ));
            bundles.Add(new ScriptBundle("~/Bundles/AuditTimeline").Include(
                      "~/Javascripts/audit-timeline.js"
                     ));

            bundles.Add(new StyleBundle("~/CSS/AuditTimelineCss").Include(
                "~/CSS/timeline.css"
            ));

            bundles.Add(new ScriptBundle("~/Bundles/ScriptsUser").Include(
                      "~/Javascripts/vendor/jquery.js",
                       "~/Javascripts/vendor/jquery.unobtrusive-ajax.min.js",
                       "~/Javascripts/dirrty.js"));

            //bundles.Add(new StyleBundle("~/CSS/DateDropper").Include(
            //    "~/Javascripts/Datedropper3-master/datedropper.min.css",
            //    "~/Javascripts/timedropper-master/timedropper.min.css"
            //));

            //bundles.Add(new ScriptBundle("~/Bundles/DateDropper").Include(
            //    "~/Javascripts/Datedropper3-master/datedropper.min.js",
            //    "~/Javascripts/timedropper-master/timedropper.min.js"
            //));

            bundles.Add(new StyleBundle("~/Fonts/Fonts").Include(
                      //"~/Fonts/MaterialIcons/material-icons.css"//,
                      //"~/Fonts/icons.min.css"
                ));

            bundles.Add(new StyleBundle("~/CSS/CSS").Include(
                      "~/CSS/foundation.css",
                      "~/CSS/app.min.css",
                      "~/CSS/paging.css",
                      "~/CSS/toastr.min.css",//toaster for popup notification
                      "~/CSS/vis.min.css"));

            //bundles.Add(new StyleBundle("~/Content/PagingCSS").Include(
            //          "~/CSS/paging.css"));
        }
    }
}