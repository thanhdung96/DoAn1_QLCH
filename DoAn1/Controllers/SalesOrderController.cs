using sqlQuanLiBanHang;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace DoAn1.Controllers
{
	public class SalesOrderController : Controller
    {
        private BH db = new BH();

        // GET: /SalesOrder/
		//public ActionResult Index()
		//{
		//	var salesorders = db.SalesOrders.Include(s => s.Customer).Include(s => s.Employee);
		//	return View(salesorders.ToList());
		//}

		[Authorize(Roles = "cashier,admin,store")]
		public ActionResult Index(string searchTerm, int? page)
		{
			//var salesorders = db.SalesOrders.Include(s => s.Customer).Include(s => s.Employee);

			var SO = from s in db.SalesOrders select s;

			if (!string.IsNullOrEmpty(searchTerm))
			{
				SO = db.SalesOrders.Where(e => e.OrderNo.Contains(searchTerm));
			}
			ViewBag.searchTerm = searchTerm;
			
			int pageSize = 20;
			int pageNumber = (page ?? 1);

			return View(SO.ToList().OrderBy(s => s.OrderNo).ToPagedList(pageNumber, pageSize));
		}

		// GET: /SalesOrder/Details/5
		[Authorize(Roles = "cashier,admin,store")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesorder = db.SalesOrders.Find(id);
            if (salesorder == null)
            {
                return HttpNotFound();
            }
            return View(salesorder);
        }

        // GET: /SalesOrder/Create
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create()
        {
			//ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustName");

			var cust = db.Customers.Where(c => c.StatusCust == "AV");
			SelectList selectListCust = new SelectList(cust, "CustID", "CustName");
			ViewBag.CustID = selectListCust;

			//ViewBag.IDEmployee = new SelectList(db.Employees, "IDEmployee", "EmployeeName");
			var employee = db.Employees.Where(c => c.StatusEmployee == "DHD");
			SelectList selectListEmployee = new SelectList(employee, "IDEmployee", "EmployeeName");
			ViewBag.IDEmployee = selectListEmployee;

			/*var id = db.SalesOrders.OrderByDescending(a => a.OrderNo).ToList();
			string newOrderNo;
			if (id.Count != 0)
			{
				newOrderNo = "S" + (int.Parse(id[0].OrderNo.Substring(2)) + 1).ToString("00");
			}
			else
			{
				newOrderNo= "S01";
			}*/
			GetID getID = new GetID();
			ViewBag.OrderNo = getID.GetNewSalesOrder();
			return View();
        }

        // POST: /SalesOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create([Bind(Include = "OrderNo,OrderDate,IDEmployee,CustID,TotalAmt,SlsDescription,OverDueDate,OrderDisc,TaxAmt,Payment,Debt,VAT")] SalesOrder salesorder)
        {
			/*var id = db.SalesOrders.OrderByDescending(a => a.OrderNo).ToList();
			if (id.Count != 0)
			{
				int new_id = int.Parse(id[0].OrderNo.Substring(2)) + 1;
				salesorder.OrderNo = "S" + new_id.ToString("00");
			}
			else
			{
				salesorder.OrderNo = "S01";
			}*/

            if (ModelState.IsValid)
            {
                db.SalesOrders.Add(salesorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

			var cust = db.Customers.Where(c => c.StatusCust == "AV");
			SelectList selectListCust = new SelectList(cust, "CustID", "CustName");
			ViewBag.CustID = selectListCust;

			//ViewBag.IDEmployee = new SelectList(db.Employees, "IDEmployee", "EmployeeName");
			var employee = db.Employees.Where(c => c.StatusEmployee == "DHD");
			SelectList selectListEmployee = new SelectList(employee, "IDEmployee", "EmployeeName");
			ViewBag.IDEmployee = selectListEmployee;

			GetID getID = new GetID();
			ViewBag.OrderNo = getID.GetNewSalesOrder();
			
			return View(salesorder);
        }

        // GET: /SalesOrder/Edit/5
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesorder = db.SalesOrders.Find(id);
            if (salesorder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustName", salesorder.CustID);
            ViewBag.IDEmployee = new SelectList(db.Employees, "IDEmployee", "EmployeeName", salesorder.IDEmployee);
            return View(salesorder);
        }

        // POST: /SalesOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit([Bind(Include = "OrderNo,OrderDate,IDEmployee,CustID,TotalAmt,SlsDescription,OverDueDate,OrderDisc,TaxAmt,Payment,Debt,VAT")] SalesOrder salesorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustName", salesorder.CustID);
            ViewBag.IDEmployee = new SelectList(db.Employees, "IDEmployee", "EmployeeName", salesorder.IDEmployee);
            return View(salesorder);
        }

        // GET: /SalesOrder/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesorder = db.SalesOrders.Find(id);
            if (salesorder == null)
            {
                return HttpNotFound();
            }
            return View(salesorder);
        }

        // POST: /SalesOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            SalesOrder salesorder = db.SalesOrders.Find(id);
            db.SalesOrders.Remove(salesorder);
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
