using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVirtualMVC.Web.Areas.Administracion.Models
{
    using System.Web.Mvc;

    using TiendaVirtualMVC.Web.Models;

    public class EditarProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public SelectList Categorias { get; set; }
        public bool TieneImagen { get; set; }

        public EditarProductoViewModel( Producto producto, 
            IList<Categoria> categorias)
        {
            this.Id = producto.Id;
            this.Nombre = producto.Nombre;
            this.Precio = producto.Precio.ToString();
            this.Categorias = 
                new SelectList(categorias,"Id","Nombre",producto.Categoria.Id);
            this.TieneImagen = producto.Imagen != null;
        }
    }
}