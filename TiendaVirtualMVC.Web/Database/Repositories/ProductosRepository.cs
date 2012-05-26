namespace TiendaVirtualMVC.Web.Database.Repositories
{
    using System.Collections.Generic;

    using TiendaVirtualMVC.Web.Database.UnitOfWork;
    using TiendaVirtualMVC.Web.Models;
    using NHibernate.Linq;
    using System.Linq;
    public class ProductosRepository:Repository<Producto>
    {
        public ProductosRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<Producto> GetEagerAll()
        {
            return this.Session.Query<Producto>()
                .Fetch(x => x.Categoria).ToList();
        }
    }
}