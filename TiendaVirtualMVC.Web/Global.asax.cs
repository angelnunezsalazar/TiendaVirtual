using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TiendaVirtualMVC.Web
{
    using NHibernate.Persistence;

    using TiendaVirtualMVC.Web.CustomModelBinders;
    using TiendaVirtualMVC.Web.Dependencies;
    using TiendaVirtualMVC.Web.Models.ViewModels;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new { controller = "Home", action = "Index", 
                    pagina = 1, categoria=UrlParameter.Optional}
            );

            routes.MapRoute(
               null, "pagina{pagina}",
               new
               {
                   controller = "Home",
                   action = "Index",
                   categoria = UrlParameter.Optional
               },
               new { pagina=@"\d+"}
            );

            routes.MapRoute(
               null, "{categoria}/pagina{pagina}",
               new { controller = "Home", action = "Index" },
               new { pagina = @"\d+" }
            ); 

            routes.MapRoute(
                null, "{categoria}",
                new {
                    controller = "Home",action = "Index",pagina = 1
                }
            );       
 
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            NHibernateConfigurator.Configure();
            ModelBinders.Binders.Add(typeof(CarroCompras),
                            new CarroComprasModelBinder());
            DependencyConfigurator.Configure();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }
    }
}