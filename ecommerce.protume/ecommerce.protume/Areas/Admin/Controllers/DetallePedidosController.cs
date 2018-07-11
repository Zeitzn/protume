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
    public class DetallePedidosController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();

        // GET: Admin/DetallePedidos
        public ActionResult Index()
        {
            var detallePedido = db.detallePedido.Include(d => d.detalleProducto).Include(d => d.pedido).OrderByDescending(x=>x.id);
            return View(detallePedido.ToList());
        }

        // GET: Admin/DetallePedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallePedido detallePedido = db.detallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // GET: Admin/DetallePedidos/Create
        public ActionResult Create()
        {
            ViewBag.id_detalle_producto = new SelectList(db.detalleProducto, "id", "id");
            //ViewBag.id_pedido = new SelectList(db.pedido, "id", "estado");
            return View();
        }

        // POST: Admin/DetallePedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_pedido,id_detalle_producto,cantidad")] detallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.detallePedido.Add(detallePedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_detalle_producto = new SelectList(db.detalleProducto, "id", "id", detallePedido.id_detalle_producto);
            ViewBag.id_pedido = new SelectList(db.pedido, "id", "estado", detallePedido.id_pedido);
            return View(detallePedido);
        }

        // GET: Admin/DetallePedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallePedido detallePedido = db.detallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_detalle_producto = new SelectList(db.detalleProducto, "id", "id", detallePedido.id_detalle_producto);
            ViewBag.id_pedido = new SelectList(db.pedido, "id", "estado", detallePedido.id_pedido);
            return View(detallePedido);
        }

        // POST: Admin/DetallePedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_pedido,id_detalle_producto,cantidad")] detallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_detalle_producto = new SelectList(db.detalleProducto, "id", "id", detallePedido.id_detalle_producto);
            ViewBag.id_pedido = new SelectList(db.pedido, "id", "estado", detallePedido.id_pedido);
            return View(detallePedido);
        }

        // GET: Admin/DetallePedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallePedido detallePedido = db.detallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // POST: Admin/DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detallePedido detallePedido = db.detallePedido.Find(id);
            db.detallePedido.Remove(detallePedido);
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
