using SmartCity.Domain.Vehicles;

namespace SmartCity.Application.Vehicles;

public sealed class CourierBikeCreator : VehicleCreator
{
    public override VehicleType VehicleType => VehicleType.CourierBike;

    protected override Vehicle CreateVehicle(Guid id, string name) =>
        new CourierBike(id, name, maxSpeedKph: 35m, maxPayloadKg: 25m);
}
