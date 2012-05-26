using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtualMVC.Web.Controllers
{
    using System.ComponentModel;

    using NHibernate.Persistence;

    using TiendaVirtualMVC.Web.Models;
    using TiendaVirtualMVC.Web.Models.ViewModels;

    public class CarroComprasController : Controller
    {
        public ActionResult Mostrar(CarroCompras carroCompras, string regresarUrl)
        {
            ViewBag.RegresarUrl = regresarUrl;
            return View(carroCompras);
        }

        [HttpPost]
        public ActionResult Agregar(CarroCompras carroCompras, int id, [DefaultValue(1)] int cantidad, string regresarUrl)
        {
            using (var session = NHibernateConfigurator.GetSession())
            using (var transaction = session.BeginTransaction())
            {
                var producto = session.Get<Producto>(id);
                carroCompras.AgregarLinea(producto, cantidad);
                transaction.Commit();
            }

            return RedirectToAction("Mostrar", new { regresarUrl });
        }

        [HttpPost]
        public ActionResult Remover(CarroCompras carroCompras, int id, string regresarUrl)
        {
            carroCompras.RemoverLinea(id);
            return RedirectToAction("Mostrar", new { regresarUrl });
        }

        public ActionResult Comprar()
        {
            return View();
        }
    }
}
