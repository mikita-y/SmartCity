namespace SmartCity.Domain.Vehicles;

public abstract class Vehicle
{
    protected Vehicle(
        Guid id,
        string name,
        decimal maxSpeedKph,
        decimal maxPayloadKg)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("A vehicle ID cannot be empty.", nameof(id));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (maxSpeedKph <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxSpeedKph),
                "Maximum speed must be greater than zero.");
        }

        if (maxPayloadKg <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxPayloadKg),
                "Maximum payload must be greater than zero.");
        }

        Id = id;
        Name = name;
        MaxSpeedKph = maxSpeedKph;
        MaxPayloadKg = maxPayloadKg;
    }

    public Guid Id { get; }

    public string Name { get; }

    public decimal MaxSpeedKph { get; }

    public decimal MaxPayloadKg { get; }
}
