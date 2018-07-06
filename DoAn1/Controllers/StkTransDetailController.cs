using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sqlQuanLiBanHang;

namespace DoAn1.Controllers
{
	public class StkTransDetailController : Controller
    {
        private BH db = new BH();

        // GET: /StkTransDetail/
		[Authorize(Roles = "invt,admin,store")]
		public ActionResult Index()
        {
            var stktransdetails = db.StkTransDetails.Include(s => s.Inventory).Include(s => s.StockTransfer);
            return View(stktransdetails.ToList());
        }

        // GET: /StkTransDetail/Details/5
		[Authorize(Roles = "invt,admin,store")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StkTransDetail stktransdetail = db.StkTransDetails.Find(id);
            if (stktransdetail == null)
            {
                return HttpNotFound();
            }
            return View(stktransdetail);
        }

        // GET: /StkTransDetail/Create
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create()
        {
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName");
            ViewBag.TransID = new SelectList(db.StockTransfers, "TransID", "FromStockID");
            return View();
        }

        // POST: /StkTransDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create([Bind(Include = "TransID,InvtID,Qty,Amount")] StkTransDetail stktransdetail)
        {
            if (ModelState.IsValid)
            {
                db.StkTransDetails.Add(stktransdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", stktransdetail.InvtID);
            ViewBag.TransID = new SelectList(db.StockTransfers, "TransID", "FromStockID", stktransdetail.TransID);
            return View(stktransdetail);
        }

        // GET: /StkTransDetail/Edit/5
		[Authorize(Roles = "invt,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StkTransDetail stktransdetail = db.StkTransDetails.Find(id);
            if (stktransdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", stktransdetail.InvtID);
            ViewBag.TransID = new SelectList(db.StockTransfers, "TransID", "FromStockID", stktransdetail.TransID);
            return View(stktransdetail);
        }

        // POST: /StkTransDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "invt,admin")]
		public ActionResult Edit([Bind(Include = "TransID,InvtID,Qty,Amount")] StkTransDetail stktransdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stktransdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", stktransdetail.InvtID);
            ViewBag.TransID = new SelectList(db.StockTransfers, "TransID", "FromStockID", stktransdetail.TransID);
            return View(stktransdetail);
        }

        // GET: /StkTransDetail/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StkTransDetail stktransdetail = db.StkTransDetails.Find(id);
            if (stktransdetail == null)
            {
                return HttpNotFound();
            }
            return View(stktransdetail);
        }

        // POST: /StkTransDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            StkTransDetail stktransdetail = db.StkTransDetails.Find(id);
            db.StkTransDetails.Remove(stktransdetail);
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
