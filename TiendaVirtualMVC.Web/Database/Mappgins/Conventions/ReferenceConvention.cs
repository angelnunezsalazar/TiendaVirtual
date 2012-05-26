namespace NHibernate.Persistence.Mappings.Conventions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    public class ReferenceConvention:IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(instance.Name + "Id");
            instance.Cascade.SaveUpdate();
        }
    }
}