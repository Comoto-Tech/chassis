# Chassis

This is a simple project as a foundation for 
for an application by providing the following services:

- Type Scanning
- Introspection
- Start Up

Assumes you want to use AutoFac DI container

## Usage

```
var app = AppFactory.Build<YourApplication>();

var app2 = AppFactory.Build<YourApplication>(typeof(AdditionalTypes).Assembly);
```

### IApplicationMarker

Provides a way to customize an application instance by the application.
This is helpful if you want to share a project but have multiple
applications in it.

### Multitenant support

Using the built in AutoFac module to support multitenant.
To use you just need to register an ITenantIdentification strategy
which returns an instance of TenantIdentifier.

Then you can load up `TenantOverrides` like Autofac `Modules` which will be
scoped to the tenant specified.