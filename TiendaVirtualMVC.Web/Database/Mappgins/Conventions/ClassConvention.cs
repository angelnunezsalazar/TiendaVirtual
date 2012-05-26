namespace NHibernate.Persistence.Mappings.Conventions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    using uNhAddIns.Inflector;

    public class ClassConvention:IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            var inflector = new SpanishInflector();
            instance.Table(inflector.Pluralize(instance.EntityType.Name));
        }
    }


}