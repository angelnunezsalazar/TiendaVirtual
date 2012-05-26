namespace NHibernate.Persistence.Mappings.Conventions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    public class IdConvention:IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column("Id");
            instance.GeneratedBy.HiLo("UniqueKey","NextHi","100");
        }
    }
}