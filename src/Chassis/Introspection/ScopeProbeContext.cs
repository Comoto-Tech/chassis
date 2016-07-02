using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chassis.Introspection
{
    public class ScopeProbeContext :
        IProbeContext
    {
        readonly CancellationToken _cancellationToken;
        readonly IDictionary<string, object> _variables;

        protected ScopeProbeContext(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _variables = new Dictionary<string, object>();
        }

        CancellationToken IProbeContext.CancellationToken {get { return _cancellationToken; } }

        void IProbeContext.Add(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (string.IsNullOrEmpty(value))
                _variables.Remove(key);
            else
                _variables[key] = value;
        }

        void IProbeContext.Add(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (value == null || (value is string && string.IsNullOrEmpty((string)value)))
                _variables.Remove(key);
            else
                _variables[key] = value;
        }

        public void Set(object values)
        {
            SetVariablesFromDictionary(GetObjectAsDictionary(values));
        }

        public void Set(IEnumerable<KeyValuePair<string, object>> values)
        {
            SetVariablesFromDictionary(values);
        }

        public IProbeContext CreateScope(string key)
        {
            var scope = new ScopeProbeContext(_cancellationToken);

            IList<ScopeProbeContext> list;

            object value;
            if (_variables.TryGetValue(key, out value))
            {
                list = value as IList<ScopeProbeContext>;
                if (list == null)
                    throw new InvalidOperationException("The key already exists and is not a scope collection: " + key);
            }
            else
            {
                list = new List<ScopeProbeContext>();
                _variables[key] = list;
            }

            list.Add(scope);
            return scope;
        }

        protected IDictionary<string, object> Build()
        {
            return _variables.ToDictionary(x => x.Key, item =>
            {
                var list = item.Value as IList<ScopeProbeContext>;
                if (list != null)
                {
                    if (list.Count == 1)
                        return list[0].Build();

                    return list.Select(x => x.Build()).ToArray();
                }

                return item.Value;
            });
        }

        void SetVariablesFromDictionary(IEnumerable<KeyValuePair<string, object>> values)
        {
            foreach (var value in values)
            {
                if (value.Value == null || (value.Value is string && string.IsNullOrEmpty((string)value.Value)))
                    _variables.Remove(value.Key);
                else
                    _variables[value.Key] = value.Value;
            }
        }

        static IEnumerable<KeyValuePair<string, object>> GetObjectAsDictionary(object values)
        {
            if (values == null)
                return new Dictionary<string, object>();

            JObject dictionary = JObject.FromObject(values, new JsonSerializer());

            return dictionary.ToObject<IDictionary<string, object>>();
        }
    }
}
