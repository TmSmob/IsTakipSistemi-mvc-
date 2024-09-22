using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMvc2.Controllers
{
    public class LoguotController : Controller
    {
        // GET: Loguot
        public ActionResult Index()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}