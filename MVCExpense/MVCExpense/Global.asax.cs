// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Michael Morris">
//  (c) Michael Morris, 2018.
// </copyright>
// <summary>Defines the MvcApplication type. </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MVCExpense
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    /// <inheritdoc />
    /// <summary>
    /// Global MVC Application handling
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Global Application start event handler
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
