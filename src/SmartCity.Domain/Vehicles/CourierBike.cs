namespace SmartCity.Domain.Vehicles;

public sealed class CourierBike : Vehicle
{
    public CourierBike(
        Guid id,
        string name,
        decimal maxSpeedKph,
        decimal maxPayloadKg)
        : base(id, name, maxSpeedKph, maxPayloadKg)
    {
    }
}
