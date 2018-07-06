using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sqlQuanLiBanHang;

namespace DoAn1.Controllers
{
	public class StockTransferController : Controller
    {
        private BH db = new BH();

        // GET: /StockTransfer/
		[Authorize(Roles = "invt,admin,store")]
		public ActionResult Index()
        {
            var stocktransfers = db.StockTransfers.Include(s => s.Stock).Include(s => s.Stock1);
            return View(stocktransfers.ToList());
        }

        // GET: /StockTransfer/Details/5
		[Authorize(Roles = "invt,admin,store")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockTransfer stocktransfer = db.StockTransfers.Find(id);
            if (stocktransfer == null)
            {
                return HttpNotFound();
            }
            return View(stocktransfer);
        }

        // GET: /StockTransfer/Create
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create()
        {
            ViewBag.FromStockID = new SelectList(db.Stocks, "StockID", "NoteStock");
            ViewBag.ToStockID = new SelectList(db.Stocks, "StockID", "NoteStock");
            return View();
        }

        // POST: /StockTransfer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create([Bind(Include = "TransID,FromStockID,ToStockID,TransDate,TotalAmt,Descript")] StockTransfer stocktransfer)
        {
            if (ModelState.IsValid)
            {
                db.StockTransfers.Add(stocktransfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromStockID = new SelectList(db.Stocks, "StockID", "NoteStock", stocktransfer.FromStockID);
            ViewBag.ToStockID = new SelectList(db.Stocks, "StockID", "NoteStock", stocktransfer.ToStockID);
            return View(stocktransfer);
        }

        // GET: /StockTransfer/Edit/5
		[Authorize(Roles = "invt,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockTransfer stocktransfer = db.StockTransfers.Find(id);
            if (stocktransfer == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromStockID = new SelectList(db.Stocks, "StockID", "NoteStock", stocktransfer.FromStockID);
            ViewBag.ToStockID = new SelectList(db.Stocks, "StockID", "NoteStock", stocktransfer.ToStockID);
            return View(stocktransfer);
        }

        // POST: /StockTransfer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "invt,admin")]
		public ActionResult Edit([Bind(Include = "TransID,FromStockID,ToStockID,TransDate,TotalAmt,Descript")] StockTransfer stocktransfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stocktransfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromStockID = new SelectList(db.Stocks, "StockID", "NoteStock", stocktransfer.FromStockID);
            ViewBag.ToStockID = new SelectList(db.Stocks, "StockID", "NoteStock", stocktransfer.ToStockID);
            return View(stocktransfer);
        }

        // GET: /StockTransfer/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockTransfer stocktransfer = db.StockTransfers.Find(id);
            if (stocktransfer == null)
            {
                return HttpNotFound();
            }
            return View(stocktransfer);
        }

        // POST: /StockTransfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            StockTransfer stocktransfer = db.StockTransfers.Find(id);
            db.StockTransfers.Remove(stocktransfer);
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
