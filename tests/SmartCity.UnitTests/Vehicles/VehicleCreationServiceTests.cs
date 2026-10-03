using SmartCity.Application.Vehicles;
using SmartCity.Domain.Vehicles;

namespace SmartCity.UnitTests.Vehicles;

public class VehicleCreationServiceTests
{
    private readonly VehicleCreationService _service = new();

    [Fact]
    public void Create_WhenDroneRequested_ReturnsConfiguredDrone()
    {
        var vehicle = _service.Create(VehicleType.Drone, "Aerial Courier");

        var drone = Assert.IsType<Drone>(vehicle);
        Assert.NotEqual(Guid.Empty, drone.Id);
        Assert.Equal("Aerial Courier", drone.Name);
        Assert.Equal(80m, drone.MaxSpeedKph);
        Assert.Equal(5m, drone.MaxPayloadKg);
    }

    [Fact]
    public void Create_WhenDeliveryRobotRequested_ReturnsConfiguredDeliveryRobot()
    {
        var vehicle = _service.Create(VehicleType.DeliveryRobot, "Sidewalk Carrier");

        var robot = Assert.IsType<DeliveryRobot>(vehicle);
        Assert.NotEqual(Guid.Empty, robot.Id);
        Assert.Equal("Sidewalk Carrier", robot.Name);
        Assert.Equal(12m, robot.MaxSpeedKph);
        Assert.Equal(50m, robot.MaxPayloadKg);
    }

    [Fact]
    public void Create_WhenCourierBikeRequested_ReturnsConfiguredCourierBike()
    {
        var vehicle = _service.Create(VehicleType.CourierBike, "City Rider");

        var bike = Assert.IsType<CourierBike>(vehicle);
        Assert.NotEqual(Guid.Empty, bike.Id);
        Assert.Equal("City Rider", bike.Name);
        Assert.Equal(35m, bike.MaxSpeedKph);
        Assert.Equal(25m, bike.MaxPayloadKg);
    }

    [Fact]
    public void Create_WhenVehicleTypeIsUnsupported_ThrowsArgumentOutOfRangeException()
    {
        var unsupportedType = (VehicleType)999;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => _service.Create(unsupportedType, "Unknown"));

        Assert.Equal("vehicleType", exception.ParamName);
        Assert.Equal(unsupportedType, exception.ActualValue);
    }

    [Fact]
    public void Create_WhenCreatorIsSupplied_UsesItsVehicleConfiguration()
    {
        var service = new VehicleCreationService([new CustomDroneCreator()]);

        var vehicle = service.Create(VehicleType.Drone, "Custom Courier");

        var drone = Assert.IsType<Drone>(vehicle);
        Assert.NotEqual(Guid.Empty, drone.Id);
        Assert.Equal("Custom Courier", drone.Name);
        Assert.Equal(60m, drone.MaxSpeedKph);
        Assert.Equal(10m, drone.MaxPayloadKg);
    }

    [Fact]
    public void Create_WhenVehicleTypeHasNoRegisteredCreator_ThrowsArgumentOutOfRangeException()
    {
        var service = new VehicleCreationService([new DroneCreator()]);

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => service.Create(VehicleType.CourierBike, "City Rider"));

        Assert.Equal("vehicleType", exception.ParamName);
        Assert.Equal(VehicleType.CourierBike, exception.ActualValue);
    }

    private sealed class CustomDroneCreator : VehicleCreator
    {
        public override VehicleType VehicleType => VehicleType.Drone;

        protected override Vehicle CreateVehicle(Guid id, string name) =>
            new Drone(id, name, maxSpeedKph: 60m, maxPayloadKg: 10m);
    }
}
