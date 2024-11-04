using Microsoft.AspNetCore.Mvc;
using BaiThiThucHanh.Models;

namespace BaiThiThucHanh.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                var u = db.TUsers.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if (u != null)
                {
                    if (u.LoaiUser == 0)
                    {
                        HttpContext.Session.SetString("Username", u.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (u.LoaiUser == 1)
                    {
                        HttpContext.Session.SetString("Username", u.Username);
                        return RedirectToAction("Index", "HomeAdmin", new { area = "admin" });
                    }
                }
            }
            return View();
        }
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(TUser user)
		{
			if (ModelState.IsValid)
			{
				db.TUsers.Add(user);
				db.SaveChanges();
				return RedirectToAction("Login");
			}
			return View();
		}

		public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }
    }
}
