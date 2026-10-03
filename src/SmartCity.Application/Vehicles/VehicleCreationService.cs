using SmartCity.Domain.Vehicles;

namespace SmartCity.Application.Vehicles;

public sealed class VehicleCreationService
{
    private readonly Dictionary<VehicleType, VehicleCreator> _creators;

    public VehicleCreationService()
        : this([new DroneCreator(), new DeliveryRobotCreator(), new CourierBikeCreator()])
    {
    }

    public VehicleCreationService(IEnumerable<VehicleCreator> creators)
    {
        ArgumentNullException.ThrowIfNull(creators);

        _creators = creators.ToDictionary(creator => creator.VehicleType);
    }

    public Vehicle Create(VehicleType vehicleType, string name)
    {
        if (!_creators.TryGetValue(vehicleType, out var creator))
        {
            throw new ArgumentOutOfRangeException(
                nameof(vehicleType),
                vehicleType,
                "The requested vehicle type is not supported.");
        }

        return creator.Create(name);
    }
}
