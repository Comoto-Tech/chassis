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