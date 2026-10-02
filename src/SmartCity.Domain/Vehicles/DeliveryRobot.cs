namespace SmartCity.Domain.Vehicles;

public sealed class DeliveryRobot : Vehicle
{
    public DeliveryRobot(
        Guid id,
        string name,
        decimal maxSpeedKph,
        decimal maxPayloadKg)
        : base(id, name, maxSpeedKph, maxPayloadKg)
    {
    }
}
