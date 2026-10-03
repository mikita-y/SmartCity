using SmartCity.Domain.Routes;

namespace SmartCity.Application.Routes;

public sealed class FastestRoutePlanningStrategy : IRoutePlanningStrategy
{
    public RoutePlanningMode Mode => RoutePlanningMode.Fastest;

    public RoutePlan Plan(RoutePlanningRequest request) =>
        RoutePlanCalculator.Calculate(Mode, request, speedFactor: 1.2m, energyFactor: 1.3m);
}
