using Autofac;
using Chassis.Types;

namespace Chassis.Tenants
{
    public abstract class TenantOverrides
    {
        public abstract TenantIdentifier TenantId { get; }

        public virtual void RegisterComponents(ContainerBuilder builder, TypePool pool)
        {
        }
    }
}