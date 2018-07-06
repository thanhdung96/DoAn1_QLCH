using sqlQuanLiBanHang;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace DoAn1.Controllers
{
	public class CustomerController : Controller
    {
        private BH db = new BH();

        // GET: /Customer/
		//public ActionResult Index()
		//{
		//	var customers = db.Customers.Include(c => c.Employee);
		//	return View(customers.ToList());
		//}

		[Authorize(Roles = "store,admin")]
		public ActionResult Index(string searchTerm, int? page)
		{
			var customers = db.Customers.Include(c => c.Employee);
			var cus = from b in db.Customers select b;

			if (!string.IsNullOrEmpty(searchTerm))
			{
				cus = db.Customers.Where(e => e.CustName.Contains(searchTerm));
			}
			ViewBag.searchTerm = searchTerm;

			int pageSize = 20;
			int pageNumber = (page ?? 1);

			return View(cus.ToList().OrderBy(e => e.CustID).ToPagedList(pageNumber, pageSize));
		}
		
		// GET: /Customer/Details/5
		[Authorize(Roles = "store,admin")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Customer/Create
		[Authorize(Roles = "store,admin")]
		public ActionResult Create()
        {
			var em = db.Employees.Where(c => c.StatusEmployee == "DHD");
            ViewBag.IDEmployee = new SelectList(em, "IDEmployee", "EmployeeName");
            return View();
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "store,admin")]
		public ActionResult Create([Bind(Include = "CustID,CustName,AddressCust,IDEmployee,Phone,MaxDebt,TimeDebt,StatusCust,Fax,Email,Overdue,Amount,OverDueAmt,DueAmt,DescriptionCust")] Customer customer)
        {
			var id = db.Customers.OrderByDescending(a => a.CustID).ToList();
			if (id.Count != 0)
			{
				int new_id = int.Parse(id[0].CustID.Substring(1,2)) + 1;
				customer.CustID= "C" + new_id.ToString("00");
			}
			else
			{
				customer.CustID = "C01";
			}

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDEmployee = new SelectList(db.Employees, "IDEmployee", "EmployeeName", customer.IDEmployee);
            return View(customer);
        }

        // GET: /Customer/Edit/5
		[Authorize(Roles = "store,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDEmployee = new SelectList(db.Employees, "IDEmployee", "EmployeeName", customer.IDEmployee);
            return View(customer);
        }

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "store,admin")]
		public ActionResult Edit([Bind(Include = "CustID,CustName,AddressCust,IDEmployee,Phone,MaxDebt,TimeDebt,StatusCust,Fax,Email,Overdue,Amount,OverDueAmt,DueAmt,DescriptionCust")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDEmployee = new SelectList(db.Employees, "IDEmployee", "EmployeeName", customer.IDEmployee);
            return View(customer);
        }

        // GET: /Customer/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
