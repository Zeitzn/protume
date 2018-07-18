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
            var pedidos = db.pedido.Include(d => d.cliente).OrderByDescending(x => x.id);

            
            return View(pedidos.ToList());
        }

        
         public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detallePedido = db.detallePedido.Where(x=>x.id_pedido==id).ToList();

            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pedido pedid = db.pedido.Find(id);
            if (pedid == null)
            {
                return HttpNotFound();
            }
            
            return View(pedid);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id,string estado)
        {
            var pedido = db.pedido.Find(id);

            pedido.estado = estado;
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
