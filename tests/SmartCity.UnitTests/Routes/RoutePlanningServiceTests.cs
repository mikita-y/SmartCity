using SmartCity.Application.Routes;
using SmartCity.Domain.Routes;

namespace SmartCity.UnitTests.Routes;

public class RoutePlanningServiceTests
{
    private readonly RoutePlanningService _service = new();

    [Fact]
    public void Plan_ForSameDelivery_EachModePrioritizesItsMetric()
    {
        var request = CreateRequest();

        var fastest = _service.Plan(RoutePlanningMode.Fastest, request);
        var cheapest = _service.Plan(RoutePlanningMode.Cheapest, request);
        var eco = _service.Plan(RoutePlanningMode.Eco, request);

        Assert.True(fastest.EstimatedDuration < cheapest.EstimatedDuration);
        Assert.True(fastest.EstimatedDuration < eco.EstimatedDuration);
        Assert.True(cheapest.EstimatedCost < fastest.EstimatedCost);
        Assert.True(cheapest.EstimatedCost < eco.EstimatedCost);
        Assert.True(eco.EstimatedEnergyUsage < fastest.EstimatedEnergyUsage);
        Assert.True(eco.EstimatedEnergyUsage < cheapest.EstimatedEnergyUsage);
    }

    [Theory]
    [InlineData(RoutePlanningMode.Fastest, 30, 11.58, 8.58)]
    [InlineData(RoutePlanningMode.Cheapest, 45, 9.78, 5.28)]
    [InlineData(RoutePlanningMode.Eco, 60, 9.96, 3.96)]
    public void Plan_ReturnsExpectedPositiveDeterministicMetrics(
        RoutePlanningMode mode, int minutes, double cost, double energy)
    {
        var request = CreateRequest();

        var plan = _service.Plan(mode, request);

        Assert.Equal(mode, plan.RouteMode);
        Assert.Equal(request.DistanceKm, plan.DistanceKm);
        Assert.Equal(TimeSpan.FromMinutes(minutes), plan.EstimatedDuration);
        Assert.Equal((decimal)cost, plan.EstimatedCost);
        Assert.Equal((decimal)energy, plan.EstimatedEnergyUsage);
        Assert.True(plan.EstimatedDuration > TimeSpan.Zero);
        Assert.True(plan.EstimatedCost > 0m);
        Assert.True(plan.EstimatedEnergyUsage > 0m);
        Assert.Equal(plan, _service.Plan(mode, request));
    }

    [Theory]
    [InlineData(0, RoutePlanningMode.Eco)]
    [InlineData(100, RoutePlanningMode.Fastest)]
    public void Plan_CheapestAdaptsToOperatingCost(int operatingCost, RoutePlanningMode expectedProfile)
    {
        var request = CreateRequest(operatingCostPerHour: operatingCost);

        var cheapest = _service.Plan(RoutePlanningMode.Cheapest, request);
        var expected = _service.Plan(expectedProfile, request);

        Assert.Equal(expected.EstimatedCost, cheapest.EstimatedCost);
        Assert.Equal(expected.EstimatedDuration, cheapest.EstimatedDuration);
        Assert.Equal(expected.EstimatedEnergyUsage, cheapest.EstimatedEnergyUsage);
        Assert.Equal(RoutePlanningMode.Cheapest, cheapest.RouteMode);
    }

    [Theory]
    [InlineData(RoutePlanningMode.Fastest)]
    [InlineData(RoutePlanningMode.Cheapest)]
    [InlineData(RoutePlanningMode.Eco)]
    public void Plan_HeavyTrafficIncreasesDurationEnergyAndCost(RoutePlanningMode mode)
    {
        var clear = _service.Plan(mode, CreateRequest(trafficLevel: 0m));
        var congested = _service.Plan(mode, CreateRequest(trafficLevel: 1m));

        Assert.True(congested.EstimatedDuration > clear.EstimatedDuration);
        Assert.True(congested.EstimatedEnergyUsage > clear.EstimatedEnergyUsage);
        Assert.True(congested.EstimatedCost > clear.EstimatedCost);
    }

    [Theory]
    [InlineData(RoutePlanningMode.Fastest)]
    [InlineData(RoutePlanningMode.Cheapest)]
    [InlineData(RoutePlanningMode.Eco)]
    public void Plan_WhenPricesAreZero_ReturnsZeroCost(RoutePlanningMode mode)
    {
        var request = new RoutePlanningRequest(24m, 0.5m, 0m, 4m, 60m, 0m);

        var plan = _service.Plan(mode, request);

        Assert.Equal(0m, plan.EstimatedCost);
        Assert.True(plan.EstimatedDuration > TimeSpan.Zero);
        Assert.True(plan.EstimatedEnergyUsage > 0m);
    }

    [Fact]
    public void Plan_NullRequest_IsRejected()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => _service.Plan(RoutePlanningMode.Fastest, null!));

        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void Plan_UnsupportedMode_IsRejected()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => _service.Plan((RoutePlanningMode)999, CreateRequest()));

        Assert.Equal("mode", exception.ParamName);
        Assert.Equal((RoutePlanningMode)999, exception.ActualValue);
    }

    [Theory]
    [InlineData(0, 0, 1, 4, 60, 6, "distanceKm")]
    [InlineData(-1, 0, 1, 4, 60, 6, "distanceKm")]
    [InlineData(24, -1, 1, 4, 60, 6, "trafficLevel")]
    [InlineData(24, 2, 1, 4, 60, 6, "trafficLevel")]
    [InlineData(24, 0, -1, 4, 60, 6, "energyPricePerKwh")]
    [InlineData(24, 0, 1, 0, 60, 6, "vehicleEfficiencyKmPerKwh")]
    [InlineData(24, 0, 1, -1, 60, 6, "vehicleEfficiencyKmPerKwh")]
    [InlineData(24, 0, 1, 4, 0, 6, "averageSpeedKph")]
    [InlineData(24, 0, 1, 4, -1, 6, "averageSpeedKph")]
    [InlineData(24, 0, 1, 4, 60, -1, "operatingCostPerHour")]
    public void Request_InvalidValues_AreRejected(
        int distance, int traffic, int price, int efficiency, int speed, int operatingCost,
        string parameterName)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new RoutePlanningRequest(distance, traffic, price, efficiency, speed, operatingCost));

        Assert.Equal(parameterName, exception.ParamName);
    }

    private static RoutePlanningRequest CreateRequest(
        decimal trafficLevel = 0.5m, decimal operatingCostPerHour = 6m) =>
        new(24m, trafficLevel, 1m, 4m, 60m, operatingCostPerHour);
}
