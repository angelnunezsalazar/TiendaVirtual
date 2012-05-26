namespace NHibernate.Persistence
{
    using System.Reflection;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate.Persistence.Mappings.Conventions.Configuration;
    using NHibernate.Tool.hbm2ddl;

    using TiendaVirtualMVC.Web.Models;

    public class NHibernateConfigurator
    {
        public static ISessionFactory SessionFactory { get; set; }

        public static void Configure()
        {
            var model = AutoMap.AssemblyOf<Entity>(new AutomappingConfiguration())
                .IgnoreBase<Entity>()
               .Conventions.AddAssembly(Assembly.GetExecutingAssembly());

            SessionFactory= Fluently.Configure()
                           .Database(MsSqlConfiguration.MsSql2008.ConnectionString(
                                    x => x.FromConnectionStringWithKey("TiendaVirtualMVC")))
                           .Mappings(m => m.AutoMappings.Add(model))
                           .ExposeConfiguration(x => new SchemaUpdate(x).Execute(false,true))
                           .BuildSessionFactory();
        }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
    }



}