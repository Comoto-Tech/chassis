﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Chassis.Types
{
    public class TypePool
    {
        readonly IList<Assembly> _assemblies;
        Lazy<IEnumerable<Type>> _types;
        readonly IList<Type> _explicitTypes;

        public TypePool(params Assembly[] assemblies)
        {
            _assemblies = new List<Assembly>(assemblies);
            _types = new Lazy<IEnumerable<Type>>(BuildUp);
            _explicitTypes = new List<Type>();
        }
        public TypePool() : this(new Assembly[0])
        {
        }

        public Assembly[] Assemblies { get { return _assemblies.ToArray(); } }

        public void AddType(Type type)
        {
            if (_explicitTypes.Contains(type)) return;

            _explicitTypes.Add(type);
            _types = new Lazy<IEnumerable<Type>>(BuildUp);
        }
        public void AddSource(Assembly assembly)
        {
            if (_assemblies.Contains(assembly)) return;

            _assemblies.Add(assembly);
            _types = new Lazy<IEnumerable<Type>>(BuildUp);
        }

        public IEnumerable<Type> Query()
        {
            return _types.Value;
        }

        public IEnumerable<Type> Scan<TScanner>() where TScanner : TypeScan, new()
        {
            var scanner = new TScanner();
            return _types.Value.Where(scanner.Matches);
        }

        public IEnumerable<Type> Scan(Func<Type, bool> predicate)
        {
            //TODO: add scanning time?
            return _types.Value.Where(predicate);
        }

        IEnumerable<Type> BuildUp()
        {
            var assTypes =  _assemblies
                .SelectMany(a => a.ExportedTypes)
               ;

            return  assTypes.Union(_explicitTypes);
        }

        public IEnumerable<Type> FindImplementorsOf<TInterface>()
        {
            return Scan(t => t.Implements<TInterface>())
                .Where(t => !t.IsAbstract);
        }

        public IEnumerable<ClosedInfo> FindClosedTypesOf(Type type)
        {
            return Query()
                .Where(t => t.IsClosedTypeOf(type))
                .Select(t => new ClosedInfo
                {
                    GenericArgs = t.BaseType.GetGenericArguments(),
                    ClosedType = t,
                    OpenType = type
                });
        }
    }
}
