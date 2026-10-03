using SmartCity.Domain.Vehicles;

namespace SmartCity.Application.Vehicles;

public sealed class DeliveryRobotCreator : VehicleCreator
{
    public override VehicleType VehicleType => VehicleType.DeliveryRobot;

    protected override Vehicle CreateVehicle(Guid id, string name) =>
        new DeliveryRobot(id, name, maxSpeedKph: 12m, maxPayloadKg: 50m);
}
