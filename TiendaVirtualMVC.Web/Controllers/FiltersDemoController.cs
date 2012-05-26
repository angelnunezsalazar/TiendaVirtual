using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtualMVC.Web.Controllers
{
    using TiendaVirtualMVC.Web.Filters;

    public class FiltersDemoController : Controller
    {
        [MensajeFilter]
        public ActionResult Index()
        {
            Response.Write("Action is running");
            return Content("Result is running");
        }
    }
}
