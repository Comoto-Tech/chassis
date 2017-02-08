﻿using System;
using System.Collections.Generic;
using Autofac;
using Chassis.Apps;
using Chassis.Features;
using Chassis.Tenants;

namespace Chassis.Meta
{
    public class ApplicationMetaData
    {
        public ApplicationMetaData(IApplicationDefinition appDef, string instance, IEnumerable<Module> loadedModules, IEnumerable<Feature> loadedFeatures, IEnumerable<TenantOverrides> loadedTenants, TimeSpan stopwatchElapsed)
        {
            Name = appDef.GetType().Name.Replace("Application","");
            Instance = instance;
            LoadedModules = loadedModules;
            LoadedFeatures = loadedFeatures;
            LoadedTenants = loadedTenants;
            TimeToBoot = stopwatchElapsed;
        }

        public string Name { get; }
        public string Instance { get; }
        public IEnumerable<Module> LoadedModules { get; }
        public IEnumerable<Feature> LoadedFeatures { get; }
        public IEnumerable<TenantOverrides> LoadedTenants { get; }
        public TimeSpan TimeToBoot { get; }

    }
}
