namespace NHibernate.Persistence.Mappings.Conventions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    public class HasManyConvention:IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(instance.EntityType.Name + "Id");
            instance.Inverse();
            instance.Cascade.AllDeleteOrphan();
        }
    }
}