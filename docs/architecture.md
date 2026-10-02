# Initial architecture

The solution begins with five intentionally minimal project boundaries:

- **SmartCity.Domain** — contains the domain model and core business rules.
- **SmartCity.Application** — contains application use cases and orchestration. It references `SmartCity.Domain`.
- **SmartCity.Infrastructure** — contains external-system and infrastructure implementations. It references `SmartCity.Application` and `SmartCity.Domain`.
- **SmartCity.Api** — is the application entry point and HTTP API. It references `SmartCity.Application`.
- **SmartCity.UnitTests** — contains unit tests. It references `SmartCity.Domain` and `SmartCity.Application`.

These projects contain no business architecture or pattern-oriented abstractions yet.
