namespace Day8;

public readonly record struct Position
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Z { get; init; }

    public double GetDistance(Position other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
    }
}