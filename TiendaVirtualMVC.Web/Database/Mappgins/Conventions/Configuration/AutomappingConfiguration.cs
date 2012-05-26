namespace NHibernate.Persistence.Mappings.Conventions.Configuration
{
    using System;

    using FluentNHibernate.Automapping;

    using TiendaVirtualMVC.Web.Models;

    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool IsComponent(Type type)
        {
            return type == typeof(Imagen);
        }

        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(Entity).Namespace;
        }
    }
}