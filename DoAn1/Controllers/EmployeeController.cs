using sqlQuanLiBanHang;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace DoAn1.Controllers
{
    public class EmployeeController : Controller
    {
        private BH db = new BH();

        // GET: /Employee/
		//public ActionResult Index()
		//{
		//	return View(db.Employees.ToList());
		//}
		[Authorize(Roles = "manager,admin,store")]
		public ActionResult Index(string searchTerm, int? page)
		{
			var em = from b in db.Employees select b;

			if(!string.IsNullOrEmpty(searchTerm))
			{
				em = db.Employees.Where(e => e.EmployeeName.Contains(searchTerm));
			}
			ViewBag.searchTerm = searchTerm;

			int pageSize = 20;
			int pageNumber = (page ?? 1);

			return View(em.ToList().OrderBy(e => e.IDEmployee).ToPagedList(pageNumber, pageSize));
		}
		
		// GET: /Employee/Details/5
		[Authorize(Roles = "manager,admin,store")]
		public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
		[Authorize(Roles = "manager,admin")]
		public ActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "manager,admin")]
		public ActionResult Create([Bind(Include = "IDEmployee,EmployeeName,AddressEmployee,MaxDebt,MaxReceive,StatusEmployee,DescriptionEmployee,Email")] Employee employee)
        {
			var id = db.Employees.OrderByDescending(a => a.IDEmployee).ToList();
			if (id.Count != 0)
			{
				int new_id = int.Parse(id[0].IDEmployee.Substring(1,2)) + 1;
				employee.IDEmployee = "E" + new_id.ToString("00");
			}
			else
			{
				employee.IDEmployee = "E01";
			}
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: /Employee/Edit/5
		[Authorize(Roles = "manager,admin")]
		public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "manager,admin")]
		public ActionResult Edit([Bind(Include = "IDEmployee,EmployeeName,AddressEmployee,MaxDebt,MaxReceive,StatusEmployee,DescriptionEmployee,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: /Employee/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
