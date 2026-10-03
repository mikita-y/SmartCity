namespace SmartCity.Domain.Routes;

public sealed record RoutePlanningRequest
{
    public RoutePlanningRequest(
        decimal distanceKm,
        decimal trafficLevel,
        decimal energyPricePerKwh,
        decimal vehicleEfficiencyKmPerKwh,
        decimal averageSpeedKph,
        decimal operatingCostPerHour)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(distanceKm);
        ArgumentOutOfRangeException.ThrowIfLessThan(trafficLevel, 0m);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(trafficLevel, 1m);
        ArgumentOutOfRangeException.ThrowIfNegative(energyPricePerKwh);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(vehicleEfficiencyKmPerKwh);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(averageSpeedKph);
        ArgumentOutOfRangeException.ThrowIfNegative(operatingCostPerHour);

        DistanceKm = distanceKm;
        TrafficLevel = trafficLevel;
        EnergyPricePerKwh = energyPricePerKwh;
        VehicleEfficiencyKmPerKwh = vehicleEfficiencyKmPerKwh;
        AverageSpeedKph = averageSpeedKph;
        OperatingCostPerHour = operatingCostPerHour;
    }

    public decimal DistanceKm { get; }

    /// <summary>Congestion from 0 (clear) to 1 (heavy traffic).</summary>
    public decimal TrafficLevel { get; }

    /// <summary>Monetary units per kWh, in the same currency as operating cost.</summary>
    public decimal EnergyPricePerKwh { get; }

    public decimal VehicleEfficiencyKmPerKwh { get; }

    public decimal AverageSpeedKph { get; }

    /// <summary>Time-based delivery expenses such as vehicle and operator costs.</summary>
    public decimal OperatingCostPerHour { get; }
}
