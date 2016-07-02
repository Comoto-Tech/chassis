using System.Diagnostics;
using Autofac;
using Chassis.Types;

namespace Chassis.Features
{
    /// <summary>
    /// This is a universal module for registering all the things
    /// </summary>
    [DebuggerDisplay("Feature: {Name}")]
    public abstract class Feature
    {
        public virtual void RegisterComponents(ContainerBuilder builder, TypePool pool)
        {
        }

        public string Name => GetType().Name.Replace("Feature","");

        public override string ToString() => Name;
    }
}
