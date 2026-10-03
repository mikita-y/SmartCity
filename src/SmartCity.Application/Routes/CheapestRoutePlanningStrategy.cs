using SmartCity.Domain.Routes;

namespace SmartCity.Application.Routes;

public sealed class CheapestRoutePlanningStrategy : IRoutePlanningStrategy
{
    public RoutePlanningMode Mode => RoutePlanningMode.Cheapest;

    public RoutePlan Plan(RoutePlanningRequest request)
    {
        // Include both extremes: the cheapest profile depends on energy and time prices.
        var cheapest = RoutePlanCalculator.Calculate(Mode, request, speedFactor: 0.8m, energyFactor: 0.8m);
        var fast = RoutePlanCalculator.Calculate(Mode, request, speedFactor: 1.2m, energyFactor: 1.3m);
        var eco = RoutePlanCalculator.Calculate(Mode, request, speedFactor: 0.6m, energyFactor: 0.6m);
        if (fast.EstimatedCost < cheapest.EstimatedCost)
        {
            cheapest = fast;
        }

        if (eco.EstimatedCost < cheapest.EstimatedCost)
        {
            cheapest = eco;
        }

        return cheapest;
    }
}
