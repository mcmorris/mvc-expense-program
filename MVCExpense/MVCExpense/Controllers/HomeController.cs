namespace MVCExpense.Controllers
{
    using System.Web.Mvc;

    /// <inheritdoc />
    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Route and return the response for an index request.
        /// </summary>
        /// <returns>ViewResult containing index data</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Route and return the response for an about request.
        /// </summary>
        /// <returns>ViewResult containing about data</returns>
        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";
            return this.View();
        }

        /// <summary>
        /// Route and return the response for an contact request.
        /// </summary>
        /// <returns>ViewResult containing contact data</returns>
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";
            return this.View();
        }
    }
}