// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Michael Morris">
//  (c) Michael Morris, 2018.
// </copyright>
// <summary>Defines the FilterConfig class.</summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MVCExpense
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// All route configurations to map to controllers
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// An existing route collection that has not yet been registered.
        /// </summary>
        /// <param name="routes">Routes to map to controllers</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                    {
                        controller = "Home",
                        action     = "Index",
                        id         = UrlParameter.Optional
                    });
        }
    }
}
