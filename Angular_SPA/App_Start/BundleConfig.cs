﻿using System;
using System.Web.Optimization;

namespace Angular_SPA
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {




            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(
              new ScriptBundle("~/scripts/vendor")
                .Include("~/scripts/jquery-{version}.js")
                .Include("~/scripts/angular.js")
                .Include("~/scripts/angular-ui-router.js")
                //.Include("~/scripts/knockout-{version}.debug.js")
                //.Include("~/scripts/sammy-{version}.js")
                //.Include("~/scripts/toastr-{version}.js")
                //.Include("~/scripts/Q.js")
                .Include("~/scripts/bootstrap.js")
                //.Include("~/scripts/moment.js")
                .Include("~/app/app.js")
                .Include("~/app/home/*-controller.js")
                .Include("~/app/home/*-service.js")
                );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/ie10mobile.css")
                .Include("~/Content/bootstrap.css")
                //.Include("~/Content/toastr.css")
                .Include("~/Content/app.css")
              );
        }


        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            //ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}