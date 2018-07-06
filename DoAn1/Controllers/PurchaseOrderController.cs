using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sqlQuanLiBanHang;
using PagedList;

namespace DoAn1.Controllers
{
	public class PurchaseOrderController : Controller
    {
        private BH db = new BH();

        // GET: /PurchaseOrder/
		//public ActionResult Index()
		//{
		//	return View(db.PurchaseOrders.ToList());
		//}

		[Authorize(Roles = "cashier,admin,store")]
		public ActionResult Index(string searchTerm, int? page)
		{
			var PO = from p in db.PurchaseOrders select p;

			if (!string.IsNullOrEmpty(searchTerm))
			{
				PO = db.PurchaseOrders.Where(e => e.OrderNO.Contains(searchTerm));
			}
			ViewBag.searchTerm = searchTerm;

			int pageSize = 20;
			int pageNumber = (page ?? 1);
			return View(PO.ToList().OrderBy(p => p.OrderNO).ToPagedList(pageNumber, pageSize));
		}

		// GET: /PurchaseOrder/Details/5
		[Authorize(Roles = "cashier,admin,store")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorder);
        }

        // GET: /PurchaseOrder/Create
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create()
        {
            return View();
        }

        // POST: /PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create([Bind(Include = "OrderNO,OrderDate,DiscAmt,TaxAmt,TotalAmt,OrverdueDate,PromAmt,ComAmt")] PurchaseOrder purchaseorder)
        {
			/*var id = db.PurchaseOrders.OrderByDescending(a => a.OrderNO).ToList();
			if (id.Count != 0)
			{
				int new_id = int.Parse(id[0].OrderNO.Substring(2)) + 1;
				purchaseorder.OrderNO= "P" + new_id.ToString("00");
			}
			else
			{
				purchaseorder.OrderNO = "P01";
			}*/
			GetID getID = new GetID();
			purchaseorder.OrderNO = getID.GetNewPurchaseOrder();

            if (ModelState.IsValid)
            {
                db.PurchaseOrders.Add(purchaseorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseorder);
        }

        // GET: /PurchaseOrder/Edit/5
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrderDate = purchaseorder.OrderDate.Value.Date;
            return View(purchaseorder);
        }

        // POST: /PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit([Bind(Include = "OrderNO,OrderDate,DiscAmt,TaxAmt,TotalAmt,OrverdueDate,PromAmt,ComAmt")] PurchaseOrder purchaseorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseorder);
        }

        // GET: /PurchaseOrder/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorder);
        }

        // POST: /PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            db.PurchaseOrders.Remove(purchaseorder);
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
