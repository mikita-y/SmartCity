using SmartCity.Domain.Routes;

namespace SmartCity.Application.Routes;

public interface IRoutePlanningStrategy
{
    RoutePlanningMode Mode { get; }

    RoutePlan Plan(RoutePlanningRequest request);
}
