namespace SmartCity.Domain.Routes;

/// <param name="EstimatedCost">Energy and time-based expenses in the request's currency.</param>
/// <param name="EstimatedEnergyUsage">Estimated energy consumption in kWh.</param>
public sealed record RoutePlan(
    RoutePlanningMode RouteMode,
    decimal DistanceKm,
    TimeSpan EstimatedDuration,
    decimal EstimatedCost,
    decimal EstimatedEnergyUsage);
