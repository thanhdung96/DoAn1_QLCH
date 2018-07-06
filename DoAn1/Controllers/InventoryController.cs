using sqlQuanLiBanHang;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace DoAn1.Controllers
{
	public class InventoryController : Controller
    {
        private BH db = new BH();

        // GET: /Inventory/
		//public ActionResult Index()
		//{
		//	var inventories = db.Inventories.Include(i => i.Unit);
		//	return View(inventories.ToList());
		//}

		[Authorize(Roles = "store,invt,admin")]
		public ActionResult Index(string searchTerm, int? page)
		{
			//var inventories = db.Inventories.Include(i => i.Unit);

			var invt = from i in db.Inventories select i;

			if (!string.IsNullOrEmpty(searchTerm))
			{
				invt = db.Inventories.Where(e => e.InvtName.Contains(searchTerm));
			}
			ViewBag.searchTerm = searchTerm;

			int pageSize = 20;
			int pageNumber = (page ?? 1);
			return View(invt.ToList().OrderBy(i => i.InvtID).ToPagedList(pageNumber, pageSize));
		}
		
		// GET: /Inventory/Details/5
		[Authorize(Roles = "invt,store,admin")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: /Inventory/Create
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create()
        {
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName");
            return View();
        }

        // POST: /Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "invt,admin")]
		public ActionResult Create([Bind(Include = "InvtID,InvtName,ClassName,UnitID,SalesPriceT,SalesPriceL,SlsTax,QtyStock,UnitRate,InvtDescription")] Inventory inventory)
        {
			var id = db.Inventories.OrderByDescending(a => a.InvtID).ToList();
			if (id.Count != 0)
			{
				int new_id = int.Parse(id[0].InvtID.Substring(2)) + 1;
				inventory.InvtID = "I" + new_id.ToString("00");
			}
			else
			{
				inventory.InvtID = "I01";
			}

            if (ModelState.IsValid)
            {
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", inventory.UnitID);
            return View(inventory);
        }

        // GET: /Inventory/Edit/5
		[Authorize(Roles = "store,invt,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", inventory.UnitID);
            return View(inventory);
        }

        // POST: /Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "store,invt,admin")]
		public ActionResult Edit([Bind(Include = "InvtID,InvtName,ClassName,UnitID,SalesPriceT,SalesPriceL,SlsTax,QtyStock,UnitRate,InvtDescription")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", inventory.UnitID);
            return View(inventory);
        }

        // GET: /Inventory/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: /Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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
