internal struct Coord : IComparable<Coord>
{
    public float Latitude;

    public float Longitude;

    public Coord(float Latitude,float Longitude)
    {
        this.Latitude = Latitude;
        this.Longitude = Longitude;
    }

    public int CompareTo(Coord other)
    {
        int latitudeComparison = Latitude.CompareTo(other.Latitude);

        if (latitudeComparison != 0)
            return latitudeComparison;

        return Longitude.CompareTo(other.Longitude);
    }
}
