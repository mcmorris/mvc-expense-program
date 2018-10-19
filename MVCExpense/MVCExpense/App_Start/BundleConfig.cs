// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Michael Morris">
//  (c) Michael Morris, 2018.
// </copyright>
// <summary>Defines the FilterConfig class.</summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MVCExpense
{
    using System.Web.Optimization;

    /// <summary>
    /// All configuration for web application bundles.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register known Bundle registrations into site bundles.
        /// </summary>
        /// <param name="bundles">A collection of bundles to add known bundles onto</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"));

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js"));

            bundles.Add(
                new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/site.css"));
        }
    }
}
