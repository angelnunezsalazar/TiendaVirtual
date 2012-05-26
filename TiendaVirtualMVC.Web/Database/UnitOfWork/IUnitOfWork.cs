namespace TiendaVirtualMVC.Web.Database.UnitOfWork
{
    using System;

    using NHibernate;

    public interface IUnitOfWork:IDisposable
    {
        ISession CurrentSession { get; }

        void Begin();

        void End();
    }
}