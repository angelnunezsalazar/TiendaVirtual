namespace TiendaVirtualMVC.Web.Database.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;
    using NHibernate.Linq;

    using TiendaVirtualMVC.Web.Database.UnitOfWork;
    using TiendaVirtualMVC.Web.Models;

    public class Repository<T> where T:Entity 
    {
        private readonly IUnitOfWork unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public T Get(int id)
        {
            return this.Session.Get<T>(id);
        }

        public void Add(T producto)
        {
            this.Session.Save(producto);
        }

        public IQueryable<T> GetAll()
        {
            return this.Session.Query<T>();
        }

        protected ISession Session
        {
            get
            {
                return this.unitOfWork.CurrentSession;
            }
        }
    }
}