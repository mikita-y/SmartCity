# SmartCity

SmartCity is an educational .NET project for studying all 23 Gang of Four (GoF) design patterns.

Patterns will be introduced incrementally through pull requests. For most patterns, the workflow will be:

1. Implement functionality without the pattern.
2. Refactor the same functionality using the pattern.
3. Document the pattern and the reasoning behind its use.

The project models a smart-city and delivery ecosystem, including deliveries, vehicles, routes, notifications, external services, and other city-related components.

The initial version contains infrastructure only. No design patterns or business functionality are implemented yet.

## Projects

- `SmartCity.Domain` — domain model and core business rules.
- `SmartCity.Application` — application use cases and orchestration.
- `SmartCity.Infrastructure` — external systems and infrastructure implementations.
- `SmartCity.Api` — application entry point and HTTP API.
- `SmartCity.UnitTests` — unit tests.

## Build and test

```shell
dotnet build
dotnet test
```
