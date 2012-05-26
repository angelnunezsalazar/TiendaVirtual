namespace TiendaVirtualMVC.Web.Models.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class CarroCompras
    {
        public List<LineaCarroCompras> detalle = new List<LineaCarroCompras>();

        public void AgregarLinea(Producto producto, int cantidad)
        {
            detalle.Add(new LineaCarroCompras
                {
                    Producto=producto,
                    Cantidad=cantidad
                });
        }


        public void RemoverLinea(int id)
        {
            detalle.RemoveAll(x => x.Producto.Id == id);
        }

        public decimal Total()
        {
            return detalle.Sum(x => x.SubTotal);
        }
    }

    public class LineaCarroCompras
    {
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal SubTotal
        {
            get
            {
                return Producto.Precio * Cantidad;
            }
        }
    }
}