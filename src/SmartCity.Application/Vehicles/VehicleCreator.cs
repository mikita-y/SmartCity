using SmartCity.Domain.Vehicles;

namespace SmartCity.Application.Vehicles;

public abstract class VehicleCreator
{
    public abstract VehicleType VehicleType { get; }

    public Vehicle Create(string name) => CreateVehicle(Guid.NewGuid(), name);

    protected abstract Vehicle CreateVehicle(Guid id, string name);
}
