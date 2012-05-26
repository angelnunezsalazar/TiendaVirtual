using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtualMVC.Web.Controllers
{
    using System.Configuration;
    using System.IO;

    using NHibernate.Persistence;

    using TiendaVirtualMVC.Web.Models;
    using NHibernate.Linq;

    using TiendaVirtualMVC.Web.Pagination;

    public class HomeController : Controller
    {
        private const int elementosPorPagina = 2;

        public ActionResult Index(int pagina, string categoria)
        {
            using (var session = NHibernateConfigurator.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    IQueryable<Producto> query = session.Query<Producto>()
                                                    .OrderBy(x => x.Nombre);
                    if (categoria != null)
                    {
                        query = query.Where(x => x.Categoria.Nombre == categoria);
                    }


                    var total = query.Count();

                    query = query.Skip((pagina - 1) * elementosPorPagina)
                        .Take(elementosPorPagina);


                    var productos = query.ToList();
                    transaction.Commit();

                    var pagedList = new PagedList(productos,
                                                 total,
                                                 pagina,
                                                 elementosPorPagina);
                    return this.View(pagedList);
                }
            }
        }

        public ActionResult Imagen(int id)
        {
            using (var session = NHibernateConfigurator.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var producto = session.Get<Producto>(id);
                string path = Path.Combine(ConfigurationManager.AppSettings["ImagenesProductos"],
                                producto.Imagen.Ruta);
                transaction.Commit();
                return File(path, producto.Imagen.Tipo);
            }
        }

    }
}
