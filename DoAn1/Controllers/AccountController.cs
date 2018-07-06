using System.Web.Mvc;
using DoAn1.Models.Security;
using System.Web.Security;

namespace DoAn1.Controllers
{
    public class AccountController : Controller
    {
		public ActionResult KhongCoQuyen()
		{
			return View();
		}

		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(Login model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (new Member().ValidateUser(model.Username1, model.Password_))
				{
					FormsAuthentication.SetAuthCookie(model.Username1, model.RememberMe);
					if (!string.IsNullOrEmpty(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						Session["LoginUserName"] = "Xin chào " + model.Username1;
 
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					model.Password_ = "";
					TempData["SaiPass"] = "Username/Password không đúng! Vui lòng nhập lại ^^";
					//ModelState.AddModelError("", "Username/Password không đúng! Vui lòng nhập lại ^^");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}
		// GET: Account
		public ActionResult Logout()
		{
			Session["LoginUserName"] = null;
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}
	}
}