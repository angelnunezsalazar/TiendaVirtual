using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtualMVC.Web.Controllers
{
    using NHibernate.Persistence;

    using TiendaVirtualMVC.Web.Models;

    public class NavegacionController : Controller
    {
        [ChildActionOnly]
        public ActionResult Menu()
        {
            using (var session=NHibernateConfigurator.GetSession())
            using(var transaction=session.BeginTransaction())
            {
                var categorias = session.QueryOver<Categoria>()
                                    .List<Categoria>();
                transaction.Commit();
                return this.View("_Menu",categorias);
            }
        }

    }
}
