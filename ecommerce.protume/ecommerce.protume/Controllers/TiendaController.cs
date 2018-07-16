using ecommerce.protume.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ecommerce.protume.Controllers
{
    public class TiendaController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();

        public ActionResult Index()
        {
           
            var producto = db.detalleProducto.Include("producto").Include("proveedor").Include("productoImagen").ToList();

            
            return View(producto.ToList());
        }

        public ActionResult Buscar(string p)
        {
            ViewBag.Parametro = p;
            var producto = db.detalleProducto.Include("producto").Include("proveedor").Include("productoImagen").Where(x=>x.producto.nombre.Contains(p) || x.producto.descripcion.Contains(p));


            return View(producto.ToList());
        }

        public ActionResult Categoria(string p)
        {
            ViewBag.Parametro = p;
            var producto = db.detalleProducto.Include("producto").Include("proveedor").Include("productoImagen").Where(x => x.producto.categoria.nombre.Contains(p));


            return View(producto.ToList());
        }


        //pendiente
        public JsonResult mostrarDetalles(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var detalleProducto = db.detalleProducto.Include("producto").Include("proveedor").Include("productoImagen").Where(x => x.id == id);
            return new JsonResult { Data = detalleProducto, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //return detalleProducto;

        }
    }
}