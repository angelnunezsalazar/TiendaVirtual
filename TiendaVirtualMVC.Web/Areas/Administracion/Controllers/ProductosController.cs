using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtualMVC.Web.Areas.Administracion.Controllers
{
    using System.Configuration;
    using System.IO;

    using NHibernate.Persistence;

    using TiendaVirtualMVC.Web.Areas.Administracion.Models;
    using TiendaVirtualMVC.Web.Database.Repositories;
    using TiendaVirtualMVC.Web.Database.UnitOfWork;
    using TiendaVirtualMVC.Web.Filters;
    using TiendaVirtualMVC.Web.Models;

    [Authorize]
    public class ProductosController : Controller
    {
        private readonly ProductosRepository productos;

        private readonly Repository<Categoria> categorias;

        //private readonly IUnitOfWork unitOfWork;

        public ProductosController(ProductosRepository productos,
            Repository<Categoria> categorias)
        {
            this.productos = productos;
            this.categorias = categorias;
        }

        [Transaction]
        public ActionResult Index()
        {
            ViewBag.CategoriaId = new SelectList(categorias.GetAll(), "Id", "Nombre");
            return this.View(productos.GetEagerAll());
        }


        public ActionResult Crear()
        {
            ViewBag.CategoriaId = new SelectList(categorias.GetAll(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [Transaction]
        public ActionResult Crear(Producto producto, int categoriaId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoriaId = new SelectList(categorias.GetAll(), "Id", "Nombre");
                return this.View();
            }
            producto.Categoria = categorias.Get(categoriaId);
            productos.Add(producto);
            if (Request.IsAjaxRequest())
            {
                return this.Json(new { id=producto.Id});
            }
            TempData["mensaje"] = string.Format("El producto {0} se ha creado correctamente", producto.Nombre);
            return RedirectToAction("index");
        }

        public ActionResult Editar(int id)
        {
            using (var session = NHibernateConfigurator.GetSession())
            using (var transction = session.BeginTransaction())
            {
                var producto = session.Get<Producto>(id);
                var categorias = session.QueryOver<Categoria>()
                                      .List<Categoria>();
                var viewModel =
                    new EditarProductoViewModel(producto, categorias);
                transction.Commit(); ;
                return this.View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Editar(int id, int categoriaId,
                                    HttpPostedFileBase archivo)
        {
            using (var session = NHibernateConfigurator.GetSession())
            using (var transaction = session.BeginTransaction())
            {
                var producto = session.Get<Producto>(id);
                UpdateModel(producto);
                producto.Categoria = session.Load<Categoria>(categoriaId);
                if (archivo != null)
                {
                    producto.Imagen = new Imagen
                        {
                            Ruta = archivo.FileName,
                            Tipo = archivo.ContentType
                        };

                    var path = Path.Combine(
                                    ConfigurationManager.AppSettings["ImagenesProductos"],
                                    producto.Imagen.Ruta);
                    archivo.SaveAs(Server.MapPath(path));
                }
                transaction.Commit();
                return RedirectToAction("Index");
            }
        }

        [Transaction]
        public ActionResult Existe(string nombre)
        {
            Producto producto = this.productos.GetAll()
                .SingleOrDefault(x => x.Nombre == nombre);

            return this.Json(producto == null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(int id)
        {
            using (var session = NHibernateConfigurator.GetSession())
            using (var transaction = session.BeginTransaction())
            {
                var producto = session.Load<Producto>(id);
                session.Delete(producto);
                transaction.Commit();
                return Content("OK");
            }
        }
    }
}
