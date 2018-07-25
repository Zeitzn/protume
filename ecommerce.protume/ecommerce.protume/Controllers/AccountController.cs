using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ecommerce.protume.Models.DataBase;

namespace ecommerce.protume.Controllers
{

    public class AccountController : Controller
    {
        private bd_comercioEntities bd = new bd_comercioEntities();
        public ActionResult Login(string correo, string contrasena)
        {
            var u = bd.cliente.FirstOrDefault(x => x.correo == correo && x.contrasena == contrasena);
            if (u != null)
            {
                Helper.SessionHelper.AddUserToSession(u.id.ToString());
            }
            return RedirectToAction("Index", "Productos");
        }

        public ActionResult Logout()
        {

            Helper.SessionHelper.DestroyUserSession();
            return RedirectToAction("Index", "Productos");
        }

        public ActionResult RegistrarCliente(cliente c)
        {
            bd.cliente.Add(c);
            bd.SaveChanges();
            //Helper.SessionHelper.AddUserToSession(c.id.ToString());
            return RedirectToAction("Index", "Productos");
        }

        public static string ObtenerNombreUsuario(string id)
        {
            int ident = Convert.ToInt32(id);
            using (var b = new bd_comercioEntities())
            {
                //return b.cliente.Find(Helper.SessionHelper.GetUser()).nombre;
                return b.cliente.Find(ident).nombre;
                //return "ss";
            }
        }
        public static string ObtenerMonedasUsuario(string id)
        {
            int ident = Convert.ToInt32(id);
            using (var b = new bd_comercioEntities())
            {
                //return b.cliente.Find(Helper.SessionHelper.GetUser()).nombre;
                return b.cliente.Find(ident).puntos.ToString();
                //return "ss";
            }
        }

    }
}