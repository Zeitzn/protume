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
    public class PedidosController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();

        // GET: Admin/Pedidos
        public ActionResult Index()
        {
            var detallePedido = db.detallePedido.Include(d => d.detalleProducto).Include(d => d.pedido).OrderByDescending(x => x.id);

            
            return View(detallePedido.ToList());
        }

        // GET: Admin/Pedidos/Details/5
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,string estado)
        {
            return View();
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
