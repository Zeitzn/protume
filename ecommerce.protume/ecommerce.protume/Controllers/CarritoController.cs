using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecommerce.protume.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        public ActionResult Index()
        {
            return View();
        }
    }
}