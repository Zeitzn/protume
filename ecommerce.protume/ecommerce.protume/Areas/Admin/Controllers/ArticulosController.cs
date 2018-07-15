using ecommerce.protume.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;
using ecommerce.protume.Models;

namespace ecommerce.protume.Areas.Admin.Controllers
{
    public class ArticulosController : Controller
    {
        private bd_comercioEntities db = new bd_comercioEntities();

    
        public ActionResult Index()
        {
            var producto = db.producto.Include(p => p.categoria);
            return View(producto.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre");
            return View();
        }

        // POST: Admin/Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_categoria,nombre,precio,stock,descripcion")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre", producto.id_categoria);
            return View(producto);
        }


        public ActionResult Edit(int? id)
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
            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre", producto.id_categoria);
            return View(producto);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_categoria,nombre,precio,stock,descripcion")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.categoria, "id", "nombre", producto.id_categoria);
            return View(producto);
        }

        //GESTION DE IMAGENES

        public ActionResult AsignarImagen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleProducto producto = db.detalleProducto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            
            return View(producto);
        }
        public JsonResult Adjuntar(int ProductoId, HttpPostedFileBase documento)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                error = ""
            };
            if (documento != null)
            {
                string adjunto = DateTime.Now.ToString("yyyyMMddHHmmss") +
                Path.GetExtension(documento.FileName);
                documento.SaveAs(Server.MapPath("~/ImgProductos/" + adjunto));

                db.productoImagen.Add(new productoImagen
                {
                    id_detalle_producto = ProductoId,
                    imagen = adjunto,
                    titulo = "Ejemplo",
                    descripcion = "Ejemplo"
                });
                db.SaveChanges();
            }
            else
            {
                respuesta.respuesta = false;
                respuesta.error = "Debe adjuntar un documento";
            }
            return Json(respuesta);
        }
        public PartialViewResult Adjuntos(int ProductoId)
        {
            return PartialView(db.productoImagen.Where(x => x.id_detalle_producto == ProductoId).ToList());
        }

        public JsonResult EliminarImagen(int ProductoImagenId)
        {
            var rpt = new ResponseModel()
            {
                respuesta = true,
                error = ""
            };
            var img = db.productoImagen.Find(ProductoImagenId);
            if (System.IO.File.Exists(Server.MapPath("~/ImgProductos/" + img.imagen)))
                System.IO.File.Delete(Server.MapPath("~/ImgProductos/" + img.imagen));
            db.productoImagen.Remove(img);
            db.SaveChanges();
            return Json(rpt);
        }

        //Gestion de proveedores

        public ActionResult AsignarProveedor(int id)
        {
            ViewBag.id_producto = new SelectList(db.producto.Where(x => x.id == id), "id", "nombre");
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre");
            ViewBag.ListaProveedores = db.proveedor.ToList();

            return View();
           
        }

        [HttpPost]
        public ActionResult AsignarProveedor([Bind(Include = "id,id_producto,id_proveedor,precio,stock")] detalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.detalleProducto.Add(detalleProducto);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.producto.Where(x => x.id == detalleProducto.id_producto), "id", "nombre");
            ViewBag.ListaProveedores = db.proveedor.ToList();
            return View();
        }

        public ActionResult Details(int id)
        {
            var producto = db.detalleProducto.Where(x=>x.id_producto==id).Include("producto").Include("proveedor");
            return View(producto.ToList());
        }

    }
   
}