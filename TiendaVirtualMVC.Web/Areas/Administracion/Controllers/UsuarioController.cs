using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtualMVC.Web.Areas.Administracion.Controllers
{
    using System.Web.Security;

    using TiendaVirtualMVC.Web.Areas.Administracion.Models;

    public class UsuarioController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
                if (!(model.Usuario == "admin" && model.Password == "password"))
                    ModelState.AddModelError("", "El usuario o password son incorrectos");

            if (!ModelState.IsValid)
                return this.View();

            FormsAuthentication
                .SetAuthCookie(model.Usuario, false);
            return this.Redirect
                (returnUrl ?? Url.Action("Index", "Productos"));
        }
    }
}
