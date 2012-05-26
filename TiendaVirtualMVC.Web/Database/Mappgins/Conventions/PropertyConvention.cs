using FluentNHibernate.Conventions;
namespace NHibernate.Persistence.Mappings.Conventions
{
    using FluentNHibernate.Conventions.Instances;

    public class PropertyConvention:IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            if (instance.Name=="Name")
            {
                instance.Length(100);
                instance.Not.Nullable();
            }
        }
    }
}