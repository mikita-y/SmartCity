using SmartCity.Domain.Routes;

namespace SmartCity.Application.Routes;

public sealed class RoutePlanningService
{
    private readonly Dictionary<RoutePlanningMode, IRoutePlanningStrategy> _strategies;

    public RoutePlanningService()
        : this([
            new FastestRoutePlanningStrategy(),
            new CheapestRoutePlanningStrategy(),
            new EcoRoutePlanningStrategy()
        ])
    {
    }

    public RoutePlanningService(IEnumerable<IRoutePlanningStrategy> strategies)
    {
        ArgumentNullException.ThrowIfNull(strategies);

        _strategies = strategies.ToDictionary(strategy => strategy.Mode);
    }

    public RoutePlan Plan(RoutePlanningMode mode, RoutePlanningRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!_strategies.TryGetValue(mode, out var strategy))
        {
            throw new ArgumentOutOfRangeException(nameof(mode), mode, "The requested route mode is not supported.");
        }

        return strategy.Plan(request);
    }
}
