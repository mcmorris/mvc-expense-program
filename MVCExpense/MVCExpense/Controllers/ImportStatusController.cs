namespace MVCExpense.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using ExpenseModel;

    public class ImportStatusController : Controller
    {
        private ExpensesModel _context;

        public ImportStatusController()
        {
            this._context = new ExpensesModel();
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var importStatuses = this._context.ImportStatus.ToList();
            return this.View(importStatuses);
        }

        public ActionResult Details(int id)
        {
            var importStatus = this._context.ImportStatus.SingleOrDefault(s => s.Id == id);
            if (importStatus == null) { return this.HttpNotFound(); }
            return this.View(importStatus);
        }
    }
}