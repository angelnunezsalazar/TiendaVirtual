namespace TiendaVirtualMVC.Web.Dependencies
{
    using System.Web.Mvc;

    using FluentNHibernate.Data;

    using NHibernate;
    using NHibernate.Persistence;

    using StructureMap;

    using TiendaVirtualMVC.Web.Database.UnitOfWork;

    public class DependencyConfigurator
    {
         public static void Configure()
         {
             ObjectFactory.Initialize(x =>
                 {
                     x.For<ISessionFactory>()
                         .Singleton()
                         .Add(NHibernateConfigurator.SessionFactory);
                     x.For<IUnitOfWork>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<UnitOfWork>();
                 });
             DependencyResolver.SetResolver
                 (new StructureMapResolver(ObjectFactory.Container));
         }
    }
}