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
    public class DetallesProductosController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();

        // GET: Admin/DetalleProductos
        public ActionResult Index()
        {
            var detalleProducto = db.detalleProducto.Include(d => d.producto).Include(d => d.proveedor);
            return View(detalleProducto.ToList());
        }

        // GET: Admin/DetalleProductos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleProducto detalleProducto = db.detalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleProducto);
        }

        // GET: Admin/DetalleProductos/Create
        public ActionResult Create(int id)
        {
            ViewBag.id_producto = new SelectList(db.producto.Where(x => x.id == id), "id", "nombre");
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre");
            ViewBag.ListaProveedores = db.proveedor.ToList();

            return View();
        }

        // POST: Admin/DetalleProductos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_producto,id_proveedor")] detalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.detalleProducto.Add(detalleProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.producto, "id", "nombre", detalleProducto.id_producto);
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre", detalleProducto.id_proveedor);
            return View(detalleProducto);
        }

        // GET: Admin/DetalleProductos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleProducto detalleProducto = db.detalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.producto, "id", "nombre", detalleProducto.id_producto);
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre", detalleProducto.id_proveedor);
            return View(detalleProducto);
        }

        // POST: Admin/DetalleProductos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_producto,id_proveedor")] detalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.producto, "id", "nombre", detalleProducto.id_producto);
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre", detalleProducto.id_proveedor);
            return View(detalleProducto);
        }

        // GET: Admin/DetalleProductos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleProducto detalleProducto = db.detalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleProducto);
        }

        // POST: Admin/DetalleProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalleProducto detalleProducto = db.detalleProducto.Find(id);
            db.detalleProducto.Remove(detalleProducto);
            db.SaveChanges();
            return RedirectToAction("Index");
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
