namespace TiendaVirtualMVC.Web.CustomModelBinders
{
    using System.Web.Mvc;

    using TiendaVirtualMVC.Web.Models.ViewModels;

    public class CarroComprasModelBinder:IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var carroCompras = (CarroCompras)controllerContext
                                .HttpContext.Session["CarroCompras"];
            if (carroCompras == null)
            {
                carroCompras = new CarroCompras();
                controllerContext.HttpContext.Session["CarroCompras"] = carroCompras;
            }
            return carroCompras;
        }
    }
}