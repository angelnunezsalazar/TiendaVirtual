namespace TiendaVirtualMVC.Web.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using StructureMap;

    public class StructureMapResolver : IDependencyResolver
    {
        private readonly IContainer container;

        public StructureMapResolver(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return container.TryGetInstance(serviceType);
            }
            return this.container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.GetAllInstances<Object>()
                .Where(x => x.GetType() == serviceType);
        }
    }
}