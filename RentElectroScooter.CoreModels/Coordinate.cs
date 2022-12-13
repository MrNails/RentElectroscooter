namespace RentElectroScooter.CoreModels;

public sealed class Coordinate : IEquatable<Coordinate>
{
    public Coordinate(float latitude, float longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public float Latitude { get; set; }
    public float Longitude { get; set; }

    public bool Equals(Coordinate? other) => !ReferenceEquals(other, null) && Longitude.Equals(other.Longitude) && Latitude.Equals(other.Latitude);

    public override string ToString() => $"[{Longitude},{Latitude}]";

    public override bool Equals(object? obj) => obj is Coordinate other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Longitude, Latitude);

    public static bool operator ==(Coordinate left, Coordinate right) => left.Equals(right);
    public static bool operator !=(Coordinate left, Coordinate right) => !left.Equals(right);
}