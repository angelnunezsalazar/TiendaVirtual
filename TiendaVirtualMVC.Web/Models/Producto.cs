using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVirtualMVC.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using TiendaVirtualMVC.Web.ValidationAttributes;

    public class Producto : Entity
    {
        [Required]
        [Remote("Existe","Productos",ErrorMessage = "El producto ya existe")]
        public virtual string Nombre { get; set; }
        [Min(5)]
        public virtual decimal Precio { get; set; }
        public virtual Imagen Imagen { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}