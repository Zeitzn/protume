using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.protume.Models.DataBase;

namespace ecommerce.protume.Controllers
{
    public class ProductosController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();
        
        // GET: Productos
        public ActionResult Index()
        {
            //var producto = db.producto.Include(p => p.categoria);

            var producto = db.detalleProducto.Include("producto").Include("proveedor").Include("productoImagen").ToList();

            //var a = (from dp in db.detalleProducto
            //         join p in db.producto on dp.id_producto equals p.id
            //         select dp).Take(5);

            ViewBag.Imagenes = db.productoImagen.ToList();


            //return Json(a,JsonRequestBehavior.AllowGet);
            return View(producto.ToList());
        }

        //public JsonResult MostrarImagen(int id)
        //{
        //    var imagenes = db.productoImagen;
        //    return Json(imagenes.ToList(),JsonRequestBehavior.AllowGet);
        //}


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
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
