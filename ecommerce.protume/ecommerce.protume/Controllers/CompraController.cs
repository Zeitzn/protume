using ecommerce.protume.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecommerce.protume.Controllers
{
    public class CompraController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Paso1()
        {
            return View();
        }
        public ActionResult Paso2()
        {
            return View();
        }
        public ActionResult Paso3()
        {
            return View();
        }
        public ActionResult Paso4()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RealizarPedido(List<PedidoProducto> p,int id)
        {
            var fecha = DateTime.Now;
            string estado = "En espera";

            pedido ped = new pedido
            {
                id_cliente = id,
                fecha = fecha,
                estado = estado
            };

            db.pedido.Add(ped);
            db.SaveChanges();

            var ultimoRegistro = db.pedido.Where(x => x.id_cliente == id).OrderByDescending(x => x.id).FirstOrDefault();
            int idPedido = ultimoRegistro.id;
            
            foreach (var item in p)
            {
                db.detallePedido.Add(new detallePedido
                {
                    id_detalle_producto=item.ProductoId,
                    id_pedido=idPedido,
                    cantidad=item.Cantidad

                });
                
            }
            db.SaveChanges();


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public class PedidoProducto
        {
            public int ProductoId { get; set; }
            public string Denominacion { get; set; }
            public int Cantidad { get; set; }
            public string Imagen { get; set; }
            public decimal Precio { get; set; }
        }


    }
}