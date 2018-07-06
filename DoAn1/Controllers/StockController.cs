using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sqlQuanLiBanHang;

namespace DoAn1.Controllers
{
	public class StockController : Controller
    {
        private BH db = new BH();

        // GET: /Stock/
		[Authorize(Roles = "invt,admin,store")]
		public ActionResult Index()
        {
            var stocks = db.Stocks.Include(s => s.Inventory).Include(s => s.Unit);
            return View(stocks.ToList());
        }

        // GET: /Stock/Details/5
		[Authorize(Roles = "invt,admin,store")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: /Stock/Create
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create()
        {
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName");
            ViewBag.UnitID_Stock = new SelectList(db.Units, "UnitID", "UnitName");
            return View();
        }

        // POST: /Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create([Bind(Include = "StockID,StockDate,NoteStock,InvtID,UnitID_Stock,Quantity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", stock.InvtID);
            ViewBag.UnitID_Stock = new SelectList(db.Units, "UnitID", "UnitName", stock.UnitID_Stock);
            return View(stock);
        }

        // GET: /Stock/Edit/5
		[Authorize(Roles = "invt,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", stock.InvtID);
            ViewBag.UnitID_Stock = new SelectList(db.Units, "UnitID", "UnitName", stock.UnitID_Stock);
            return View(stock);
        }

        // POST: /Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "invt,admin")]
		public ActionResult Edit([Bind(Include = "StockID,StockDate,NoteStock,InvtID,UnitID_Stock,Quantity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", stock.InvtID);
            ViewBag.UnitID_Stock = new SelectList(db.Units, "UnitID", "UnitName", stock.UnitID_Stock);
            return View(stock);
        }

        // GET: /Stock/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: /Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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
