using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.protume.Models.DataBase;

namespace ecommerce.protume.Areas.Admin.Controllers
{
    public class ProveedoresController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();


        public ActionResult Index()
        {
            var proveedor = db.proveedor;
            return View(proveedor.ToList());
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,direccion,telefono")] proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.proveedor.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(proveedor);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
          
            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,direccion,telefono")] proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(proveedor);
        }

        

        //Gestion de productos

        public ActionResult AsignarProducto(int id)
        {
            ViewBag.id_proveedor = new SelectList(db.proveedor.Where(x => x.id == id), "id", "nombre");
            ViewBag.id_producto = new SelectList(db.producto, "id", "nombre");
            ViewBag.ListaProductos = db.producto.ToList();

            return View();

        }

        [HttpPost]
        public ActionResult AsignarProducto([Bind(Include = "id,id_producto,id_proveedor,precio,stock")] detalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.detalleProducto.Add(detalleProducto);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.id_proveedor = new SelectList(db.proveedor.Where(x => x.id == detalleProducto.id_proveedor), "id", "nombre");
            ViewBag.ListaProductos = db.producto.ToList();
            return View();
        }

        public ActionResult Details(int id)
        {
            var proveedor = db.detalleProducto.Where(x => x.id_proveedor == id).Include("producto").Include("proveedor");
            return View(proveedor.ToList());
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
