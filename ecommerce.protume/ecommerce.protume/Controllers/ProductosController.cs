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

    


        //pendiente
        public ActionResult Detalles(int id)
        {
            var detallesProducto = db.detalleProducto.Include("producto").Include("proveedor").Include("productoImagen").Where(x => x.id == id).FirstOrDefault();           
            ViewBag.Comentarios = db.comentario.Include("detalleProducto").Include("cliente").Where(x => x.id_detalle_producto == id).OrderByDescending(x=>x.id);
            return View(detallesProducto);

        }

        public string Comentario(int id_cliente,int id_detalle_producto,string descripcion)
        {

            db.comentario.Add(
                new comentario
                {
                    id_cliente=id_cliente,
                    id_detalle_producto=id_detalle_producto,
                    descripcion=descripcion,
                    fecha=DateTime.Now
                }
                
                );

            db.SaveChanges();

            return "success";
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
