using SmartCity.Domain.Vehicles;

namespace SmartCity.Application.Vehicles;

public sealed class DroneCreator : VehicleCreator
{
    public override VehicleType VehicleType => VehicleType.Drone;

    protected override Vehicle CreateVehicle(Guid id, string name) =>
        new Drone(id, name, maxSpeedKph: 80m, maxPayloadKg: 5m);
}
