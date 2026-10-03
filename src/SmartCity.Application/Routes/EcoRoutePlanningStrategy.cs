using SmartCity.Domain.Routes;

namespace SmartCity.Application.Routes;

public sealed class EcoRoutePlanningStrategy : IRoutePlanningStrategy
{
    public RoutePlanningMode Mode => RoutePlanningMode.Eco;

    public RoutePlan Plan(RoutePlanningRequest request) =>
        RoutePlanCalculator.Calculate(Mode, request, speedFactor: 0.6m, energyFactor: 0.6m);
}
