using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sqlQuanLiBanHang;
using PagedList;

namespace DoAn1.Controllers
{
    public class UserNameController : Controller
    {
        private BH db = new BH();

        // GET: /UserName/
		//public ActionResult Index()
		//{
		//	return View(db.UserNames.ToList());
		//}

		[Authorize(Roles = "manager,store,admin")]
		public ActionResult Index(string searchTerm, int? page)
		{
			var un = from b in db.UserNames select b;

			if (!string.IsNullOrEmpty(searchTerm))
			{
				un = db.UserNames.Where(b => b.Username1.Contains(searchTerm) || b.MaNV.Contains(searchTerm));
			}
			ViewBag.SearchTerm = searchTerm;

			int pagesize = 20;
			int pagenumber = (page ?? 1);

			return View(un.ToList().OrderBy(i => i.Username1).ToPagedList(pagenumber, pagesize));
		}
		
		// GET: /UserName/Details/5
		[Authorize(Roles = "manager,store,admin")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserName username = db.UserNames.Find(id);
            if (username == null)
            {
                return HttpNotFound();
            }
            return View(username);
        }

        // GET: /UserName/Create
		[Authorize(Roles = "manager,admin")]
		public ActionResult Create()
        {
			ViewBag.MaNV = new SelectList(db.Employees, "IDEmployee", "EmployeeName");
            return View();
        }

        // POST: /UserName/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "manager,admin")]
		public ActionResult Create([Bind(Include = "Username1,Password_,Roles,MaNV")] UserName username)
        {
            if (ModelState.IsValid)
            {
                db.UserNames.Add(username);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(username);
        }

        // GET: /UserName/Edit/5
		[Authorize(Roles = "manager,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserName username = db.UserNames.Find(id);
            if (username == null)
            {
                return HttpNotFound();
            }
            return View(username);
        }

        // POST: /UserName/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "manager,admin")]
		public ActionResult Edit([Bind(Include = "Username1,Password_,Roles,MaNV")] UserName username)
        {
            if (ModelState.IsValid)
            {
                db.Entry(username).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(username);
        }

        // GET: /UserName/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserName username = db.UserNames.Find(id);
            if (username == null)
            {
                return HttpNotFound();
            }
            return View(username);
        }

        // POST: /UserName/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            UserName username = db.UserNames.Find(id);
            db.UserNames.Remove(username);
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
