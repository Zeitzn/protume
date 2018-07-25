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
            if (Request.Cookies["userInfo"] != null)
            {
                var id = Convert.ToInt32(Request.Cookies["userInfo"]["id"].ToString());
                var client=db.cliente.Where(x => x.id == id).FirstOrDefault();
                return View(client);
            }

            return View();

        }
        
        [HttpPost]
        public JsonResult RealizarPedido(List<PedidoProducto> p,int id,string tipo)
        {
            var fecha = DateTime.Now;
            string estado="En Espera";

            pedido ped = new pedido();

            if (tipo== "Contra EntregaPedido" || tipo == "Contra EntregaMoneda")
            {
                ped.id_cliente = id;
                ped.fecha = fecha;
                ped.estado = estado;
                ped.tipo_pago = "Contra Entrega";
            }
            else
            {
                ped.id_cliente = id;
                ped.fecha = fecha;
                ped.estado = estado;
                ped.tipo_pago = "PayPal";
            }

            

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

               var dp= db.detalleProducto.Where(x => x.id == item.ProductoId).FirstOrDefault();
                var stockActual = Convert.ToInt32(dp.stock);
                var stockNuevo = stockActual-item.Cantidad;

                dp.stock = stockNuevo.ToString();


                db.SaveChanges();

            }
            
            if (tipo=="Contra EntregaPedido" || tipo=="PayPalPedido")
            {
                var client = db.cliente.Where(x => x.id == id).FirstOrDefault();
                int? points = client.puntos;
                client.puntos = points + 10;

                db.SaveChanges();
            }
            else
            {
                var client = db.cliente.Where(x => x.id == id).FirstOrDefault();
                int? points = client.puntos;
                client.puntos = points - 100;

                db.SaveChanges();
            }
            


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