namespace TiendaVirtualMVC.Web.Models
{
    using System.Collections.Generic;

    public class Categoria:Entity
    {
        public virtual string Nombre { get; set; }
        public virtual IList<Producto> Productos { get; set; }
    }
}