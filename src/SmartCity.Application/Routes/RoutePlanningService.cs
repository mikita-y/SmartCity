using SmartCity.Domain.Routes;

namespace SmartCity.Application.Routes;

public sealed class RoutePlanningService
{
    public RoutePlan Plan(RoutePlanningMode mode, RoutePlanningRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Profiles trade speed for energy consumption on the same delivery distance.
        switch (mode)
        {
            case RoutePlanningMode.Fastest:
                return CalculatePlan(1.2m, 1.3m);
            case RoutePlanningMode.Cheapest:
                // Include both extremes: the cheapest profile depends on energy and time prices.
                var cheapest = CalculatePlan(0.8m, 0.8m);
                var fast = CalculatePlan(1.2m, 1.3m);
                var eco = CalculatePlan(0.6m, 0.6m);
                if (fast.EstimatedCost < cheapest.EstimatedCost)
                {
                    cheapest = fast;
                }

                if (eco.EstimatedCost < cheapest.EstimatedCost)
                {
                    cheapest = eco;
                }

                return cheapest;
            case RoutePlanningMode.Eco:
                return CalculatePlan(0.6m, 0.6m);
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, "The requested route mode is not supported.");
        }

        RoutePlan CalculatePlan(decimal speedFactor, decimal energyFactor)
        {
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
}
