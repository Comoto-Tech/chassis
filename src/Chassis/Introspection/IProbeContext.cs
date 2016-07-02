﻿using System.Collections.Generic;
using System.Threading;

namespace Chassis.Introspection
{
    /// <summary>
    /// Passed to a probe site to inspect it for interesting things
    /// </summary>
    public interface IProbeContext
    {
        /// <summary>
        /// If for some reason the probe is cancelled, allowing an early withdrawl
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Add a key/value pair to the current probe context
        /// </summary>
        /// <param name="key">The key name</param>
        /// <param name="value">The value</param>
        void Add(string key, string value);

        /// <summary>
        /// Add a key/value pair to the current probe context
        /// </summary>
        /// <param name="key">The key name</param>
        /// <param name="value">The value</param>
        void Add(string key, object value);

        /// <summary>
        /// Add the properties of the object as key/value pairs to the current context
        /// </summary>
        /// <param name="values">The object (typically anonymous with new{}</param>
        void Set(object values);

        /// <summary>
        /// Add the values from the enumeration as key/value pairs
        /// </summary>
        /// <param name="values"></param>
        void Set(IEnumerable<KeyValuePair<string, object>> values);

        IProbeContext CreateScope(string key);
    }
}