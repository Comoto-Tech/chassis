using System.Diagnostics;

namespace Chassis.Tenants
{
    [DebuggerDisplay("{_identifier}")]
    public class TenantIdentifier
    {
        readonly string _identifier;

        public TenantIdentifier(string identifier)
        {
            _identifier = identifier;
        }

        public override string ToString()
        {
            return _identifier;
        }

        protected bool Equals(TenantIdentifier other)
        {
            return string.Equals(_identifier, other._identifier);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TenantIdentifier) obj);
        }

        public override int GetHashCode()
        {
            return (_identifier != null ? _identifier.GetHashCode() : 0);
        }
    }
}
