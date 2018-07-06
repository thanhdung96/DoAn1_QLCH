using sqlQuanLiBanHang;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DoAn1.Controllers
{
	[Authorize(Roles = "admin")]
	public class VendorController : Controller
    {
        private BH db = new BH();

        // GET: /Vendor/
        public ActionResult Index()
        {
            var vendors = db.Vendors.Include(v => v.Inventory);
            return View(vendors.ToList());
        }

        // GET: /Vendor/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // GET: /Vendor/Create
        public ActionResult Create()
        {
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName");
            return View();
        }

        // POST: /Vendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="VendorID,VendorName,AddressVendor,Email,Phone,Fax,DueAmt,Amount,StatusVendor,DescriptionVendor,InvtID")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", vendor.InvtID);
            return View(vendor);
        }

        // GET: /Vendor/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", vendor.InvtID);
            return View(vendor);
        }

        // POST: /Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="VendorID,VendorName,AddressVendor,Email,Phone,Fax,DueAmt,Amount,StatusVendor,DescriptionVendor,InvtID")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvtID = new SelectList(db.Inventories, "InvtID", "InvtName", vendor.InvtID);
            return View(vendor);
        }

        // GET: /Vendor/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: /Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
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
