namespace SmartCity.Domain.Vehicles;

public sealed class Drone : Vehicle
{
    public Drone(
        Guid id,
        string name,
        decimal maxSpeedKph,
        decimal maxPayloadKg)
        : base(id, name, maxSpeedKph, maxPayloadKg)
    {
    }
}
