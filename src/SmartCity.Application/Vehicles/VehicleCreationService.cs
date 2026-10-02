using SmartCity.Domain.Vehicles;

namespace SmartCity.Application.Vehicles;

public sealed class VehicleCreationService
{
    public Vehicle Create(VehicleType vehicleType, string name) =>
        vehicleType switch
        {
            VehicleType.Drone => new Drone(
                Guid.NewGuid(),
                name,
                maxSpeedKph: 80m,
                maxPayloadKg: 5m),
            VehicleType.DeliveryRobot => new DeliveryRobot(
                Guid.NewGuid(),
                name,
                maxSpeedKph: 12m,
                maxPayloadKg: 50m),
            VehicleType.CourierBike => new CourierBike(
                Guid.NewGuid(),
                name,
                maxSpeedKph: 35m,
                maxPayloadKg: 25m),
            _ => throw new ArgumentOutOfRangeException(
                nameof(vehicleType),
                vehicleType,
                "The requested vehicle type is not supported.")
        };
}
