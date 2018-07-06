using sqlQuanLiBanHang;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DoAn1.Controllers
{
	public class PurchaseOrdDetaiController : Controller
    {
        private BH db = new BH();

        // GET: /PurchaseOrdDetai/
		[Authorize(Roles = "cashier,store,admin")]
		public ActionResult Index()
        {
            var purchaseorddetails = db.PurchaseOrdDetails.Include(p => p.Inventory).Include(p => p.PurchaseOrder).Include(p => p.Unit);
            return View(purchaseorddetails.ToList());
        }


		[Authorize(Roles = "cashier,store,admin")]
		public ActionResult IndexPartial(string purchaseOrderNo, bool modeDetail)
		{
			var purchaseorddetails = db.PurchaseOrdDetails.Include(p => p.Inventory).Include(p => p.PurchaseOrder).Include(p => p.Unit);
			if(!string.IsNullOrEmpty(purchaseOrderNo)){
				purchaseorddetails = purchaseorddetails.Where(m => m.OrderNO == purchaseOrderNo);
			}
			ViewBag.purchaseOrderNo = purchaseOrderNo;		//moi sua
			ViewBag.modeDetail = modeDetail;
			return PartialView(purchaseorddetails.ToList());
		}

        // GET: /PurchaseOrdDetai/Details/5
		[Authorize(Roles = "cashier,admin,store")]
		public ActionResult Details(string id, string invID, string unitID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			PurchaseOrdDetail purchaseorddetail = db.PurchaseOrdDetails.Find(id, invID, unitID);
            if (purchaseorddetail == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorddetail);
        }

        // GET: /PurchaseOrdDetai/Create
		[HttpGet]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create(string purchaseOrderNO)
        {
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName");
            ViewBag.OrderNO = new SelectList(db.PurchaseOrders, "OrderNO", "OrderNO");
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName");
			ViewBag.purchaseOrderNO = purchaseOrderNO;	//moi sua
            return View();
        }

		// POST: /PurchaseOrdDetai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create([Bind(Include = "OrderNO,InvtID,UnitID,Qty,PurchasePrice,Amount,QtyProm,QtyPromAmt,AmtProm,DiscAmt1,TaxAmt1,DiscAmt,TaxAmt")] PurchaseOrdDetail purchaseorddetail)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrdDetails.Add(purchaseorddetail);
                db.SaveChanges();
                //return RedirectToAction("Index");
				return RedirectToAction("Edit", "PurchaseOrder", new { id = purchaseorddetail.OrderNO });//~/HoaDon/Detail
			}

            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", purchaseorddetail.InvtID);
            ViewBag.OrderNO = new SelectList(db.PurchaseOrders, "OrderNO", "OrderNO", purchaseorddetail.OrderNO);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", purchaseorddetail.UnitID);
            return View(purchaseorddetail);
        }

        // GET: /PurchaseOrdDetai/Edit/5
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit(string id, string invID, string unitID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrdDetail purchaseorddetail = db.PurchaseOrdDetails.Find(id, invID, unitID);
            if (purchaseorddetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", purchaseorddetail.InvtID);
            ViewBag.OrderNO = new SelectList(db.PurchaseOrders, "OrderNO", "OrderNO", purchaseorddetail.OrderNO);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", purchaseorddetail.UnitID);
            return View(purchaseorddetail);
        }

        // POST: /PurchaseOrdDetai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit([Bind(Include = "OrderNO,InvtID,UnitID,Qty,PurchasePrice,Amount,QtyProm,QtyPromAmt,AmtProm,DiscAmt1,TaxAmt1,DiscAmt,TaxAmt")] PurchaseOrdDetail purchaseorddetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseorddetail).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
				return RedirectToAction("Edit", "PurchaseOrder", new { id = purchaseorddetail.OrderNO });//~/HoaDon/Detail
			}
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", purchaseorddetail.InvtID);
            ViewBag.OrderNO = new SelectList(db.PurchaseOrders, "OrderNO", "OrderNO", purchaseorddetail.OrderNO);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", purchaseorddetail.UnitID);
            return View(purchaseorddetail);
        }

        // GET: /PurchaseOrdDetai/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id, string invID, string unitID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrdDetail purchaseorddetail = db.PurchaseOrdDetails.Find(id,invID, unitID);
            if (purchaseorddetail == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorddetail);
        }

        // POST: /PurchaseOrdDetai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id, string invID, string unitID)
        {
            PurchaseOrdDetail purchaseorddetail = db.PurchaseOrdDetails.Find(id, invID, unitID);
            db.PurchaseOrdDetails.Remove(purchaseorddetail);
            db.SaveChanges();
			return RedirectToAction("Edit", "PurchaseOrder", new { id = purchaseorddetail.OrderNO });//~/HoaDon/Detail
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
