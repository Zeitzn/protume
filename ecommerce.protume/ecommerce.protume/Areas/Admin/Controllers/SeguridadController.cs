using ecommerce.protume.Models.DataBase;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace ecommerce.protume.Areas.Admin.Controllers
{
    public class SeguridadController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string contrasena)
        {
            var usuario = db.usuario.Where(x => x.correo == correo && x.contrasena == contrasena).FirstOrDefault();

            if (usuario != null)
            {
                SessionHelper.AddUserToSession(usuario.id.ToString());
                //return RedirectToAction("Login");
                return new RedirectToRouteResult(new RouteValueDictionary(

                    new
                    {
                        area = "Admin",
                        controller = "Usuarios",
                        action = "Index"
                    }
                    ));
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {

            Helper.SessionHelper.DestroyUserSession();
            //return RedirectToAction("Index", "Productos");
            return new RedirectToRouteResult(new RouteValueDictionary(

                    new
                    {
                        area = "Admin",
                        controller = "Seguridad",
                        action = "Login"
                    }
                    ));
        }
    }
}