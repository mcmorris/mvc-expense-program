// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="Michael Morris">
//  (c) Michael Morris, 2018.
// </copyright>
// <summary>Defines the FilterConfig class.</summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MVCExpense
{
    using System.Web.Mvc;

    /// <summary>
    /// Handles all filter configuration and tracking.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Add specified filters to global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
