using SmartCity.Domain.Routes;

namespace SmartCity.Application.Routes;

internal static class RoutePlanCalculator
{
    internal static RoutePlan Calculate(
        RoutePlanningMode mode,
        RoutePlanningRequest request,
        decimal speedFactor,
        decimal energyFactor)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Heavy traffic doubles travel time and adds 20% stop-start energy usage.
        var durationHours = request.DistanceKm * (1m + request.TrafficLevel)
            / (request.AverageSpeedKph * speedFactor);
        var energyKwh = request.DistanceKm / request.VehicleEfficiencyKmPerKwh
            * energyFactor * (1m + 0.2m * request.TrafficLevel);
        var cost = energyKwh * request.EnergyPricePerKwh
            + durationHours * request.OperatingCostPerHour;

        return new RoutePlan(
            mode,
            request.DistanceKm,
            TimeSpan.FromHours((double)durationHours),
            cost,
            energyKwh);
    }
}
