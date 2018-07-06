using sqlQuanLiBanHang;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DoAn1.Controllers
{
	public class SlsOrderDetailController : Controller
    {
        private BH db = new BH();

        // GET: /SlsOrderDetail/
		[Authorize(Roles = "cashier,admin,store")]
		public ActionResult Index()
        {
            var slsorderdetails = db.SlsOrderDetails.Include(s => s.Inventory).Include(s => s.SalesOrder).Include(s => s.Unit);
            return View(slsorderdetails.ToList());
        }

		public ActionResult IndexPartial(string salesOrderNo,bool modeDetail)
		{
			var slsorderdetails = db.SlsOrderDetails.Include(s => s.Inventory).Include(s => s.SalesOrder).Include(s => s.Unit);
			if(!string.IsNullOrEmpty(salesOrderNo)){
				slsorderdetails = slsorderdetails.Where(m => m.OrderNo == salesOrderNo);
			}
			ViewBag.salesOrderNo = salesOrderNo;		//moi sua
			ViewBag.modeDetail = modeDetail;
			return PartialView(slsorderdetails.ToList());
		}

		// GET: /SlsOrderDetail/Details/5
		[Authorize(Roles = "cashier,admin,store")]
		public ActionResult Details(string id, string invID, string unitID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlsOrderDetail slsorderdetail = db.SlsOrderDetails.Find(id, invID, unitID);
            if (slsorderdetail == null)
            {
                return HttpNotFound();
            }
            return View(slsorderdetail);
        }

        // GET: /SlsOrderDetail/Create
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create(string salesOrderNo)
        {
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName");
			ViewBag.OrderNo = new SelectList(db.SalesOrders, "OrderNo", "OrderNo");
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName");
			ViewBag.salesOrderNo = salesOrderNo;		//moi sua
            return View();
        }

        // POST: /SlsOrderDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Create([Bind(Include = "OrderNo,InvtID,UnitID,Qty,SalesPrice,Amount,Discount,DiscAmt1,TaxAmt1,DiscAmt,TaxAmt")] SlsOrderDetail slsorderdetail)
        {
            if (ModelState.IsValid)
            {
				/*them 10.06.2017*/
				var item = db.Inventories.Find(slsorderdetail.InvtID);
				slsorderdetail.SalesPrice = slsorderdetail.UnitID == "U01" ? item.SalesPriceT : item.SalesPriceL;
				/*den day*/
                db.SlsOrderDetails.Add(slsorderdetail);
                db.SaveChanges();
                //return RedirectToAction("Index");
				return RedirectToAction("Edit", "SalesOrder", new { id = slsorderdetail.OrderNo });
			}

            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", slsorderdetail.InvtID);
            ViewBag.OrderNo = new SelectList(db.SalesOrders, "OrderNo", "IDEmployee", slsorderdetail.OrderNo);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", slsorderdetail.UnitID);
            return View(slsorderdetail);
        }

        // GET: /SlsOrderDetail/Edit/5
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit(string id, string invID, string unitID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			SlsOrderDetail slsorderdetail = db.SlsOrderDetails.Find(id, invID, unitID);
            if (slsorderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", slsorderdetail.InvtID);
            ViewBag.OrderNo = new SelectList(db.SalesOrders, "OrderNo", "IDEmployee", slsorderdetail.OrderNo);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", slsorderdetail.UnitID);
            return View(slsorderdetail);
        }

        // POST: /SlsOrderDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "cashier,admin")]
		public ActionResult Edit([Bind(Include = "OrderNo,InvtID,UnitID,Qty,SalesPrice,Amount,Discount,DiscAmt1,TaxAmt1,DiscAmt,TaxAmt")] SlsOrderDetail slsorderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slsorderdetail).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
				return RedirectToAction("Edit", "SalesOrder", new { id = slsorderdetail.OrderNo });
			}
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", slsorderdetail.InvtID);
            ViewBag.OrderNo = new SelectList(db.SalesOrders, "OrderNo", "IDEmployee", slsorderdetail.OrderNo);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", slsorderdetail.UnitID);
            return View(slsorderdetail);
        }

        // GET: /SlsOrderDetail/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id, string invID, string unitID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			SlsOrderDetail slsorderdetail = db.SlsOrderDetails.Find(id, invID, unitID);
            if (slsorderdetail == null)
            {
                return HttpNotFound();
            }
            return View(slsorderdetail);
        }

        // POST: /SlsOrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id, string invID, string unitID)
        {
			SlsOrderDetail slsorderdetail = db.SlsOrderDetails.Find(id, invID, unitID);
            db.SlsOrderDetails.Remove(slsorderdetail);
            db.SaveChanges();
            //return RedirectToAction("Index");
			return RedirectToAction("Edit", "SalesOrder", new { id = slsorderdetail.OrderNo });
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
