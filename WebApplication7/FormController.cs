using Microsoft.AspNetCore.Mvc;

namespace WebApplication7
{
    public class FormController : Controller
    {

        public IActionResult Index()
        {
            return View("~/Views/Index.cshtml");
        }

        [HttpPost]
        public IActionResult SetCookie(string value, DateTime expiry)
        {
            
            
            
            CookieOptions options = new CookieOptions
            {
                Expires = expiry
            };

            Response.Cookies.Append("MyCookie", value, options);

            return RedirectToAction("CheckCookie");
        }

    
        public IActionResult CheckCookie()
        {
            var value = Request.Cookies["MyCookie"];            

            if (value != null)
            {
                ViewBag.CookieValue = value;
                
            }
            else
            {
                ViewBag.CookieValue = "Cookie not found.";
            }

            return View("~/Views/Cookie.cshtml");
        }
    }
}
