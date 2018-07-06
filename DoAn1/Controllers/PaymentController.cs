using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sqlQuanLiBanHang;

namespace DoAn1.Controllers
{
	[Authorize(Roles = "admin")]
	public class PaymentController : Controller
    {
        private BH db = new BH();

        // GET: /Payment/
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Customer).Include(p => p.Employee);
            return View(payments.ToList());
        }

        // GET: /Payment/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: /Payment/Create
        public ActionResult Create()
        {
            ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustName");
            ViewBag.SalesPersonID = new SelectList(db.Employees, "IDEmployee", "EmployeeName");
            return View();
        }

        // POST: /Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PaymentNo,CustID,SalesPersonID,PaymentDate,PaymentAmt,Descript")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustName", payment.CustID);
            ViewBag.SalesPersonID = new SelectList(db.Employees, "IDEmployee", "EmployeeName", payment.SalesPersonID);
            return View(payment);
        }

        // GET: /Payment/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustName", payment.CustID);
            ViewBag.SalesPersonID = new SelectList(db.Employees, "IDEmployee", "EmployeeName", payment.SalesPersonID);
            return View(payment);
        }

        // POST: /Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PaymentNo,CustID,SalesPersonID,PaymentDate,PaymentAmt,Descript")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustID = new SelectList(db.Customers, "CustID", "CustName", payment.CustID);
            ViewBag.SalesPersonID = new SelectList(db.Employees, "IDEmployee", "EmployeeName", payment.SalesPersonID);
            return View(payment);
        }

        // GET: /Payment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: /Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
