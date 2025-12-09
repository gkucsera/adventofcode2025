// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

var points = File.ReadAllLines("input.txt").Select(item =>
{
    var split = item.Split(',');
    return new Point(long.Parse(split[0]), long.Parse(split[1]));
}).ToArray();
var sortedList = new Dictionary<long, Line>();
var max = 0L;
for (var i = 0; i < points.Length - 1; i++)
{
    for (var j = i + 1; j < points.Length; j++)
    {
        var point1 = points[i];
        var point2 = points[j];

        var sideA = Math.Abs(point1.X - point2.X) + 1;
        var sideB = Math.Abs(point1.Y - point2.Y) + 1;
        var area = sideA * sideB;
        sortedList[area] = new Line(point1, point2);
        if (area > max)
        {
            max = area;
        }
    }
}

Console.WriteLine(max);
var sides = new List<Line>();
var previous = points.Last();
for (var i = 0; i < points.Length - 1; i++)
{
    var point = points[i];
    sides.Add(new Line(previous, point));
    previous = point;
}

max = 0L;
var stopwatch = new Stopwatch();
stopwatch.Start();
foreach (var item in sortedList.OrderByDescending(item => item.Key))
{
    if (HasRectangleIntersections(item.Value.Start, item.Value.End))
        continue;

    max = item.Key;
    break;
}

stopwatch.Stop();
Console.WriteLine(max);
Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
return;

bool HasRectangleIntersections(Point point1, Point point2)
{
    var xDiff = Math.Abs(point1.X - point2.X);
    var yDiff = Math.Abs(point1.Y - point2.Y);

    if (xDiff == 0 || yDiff == 0)
        return true;

    var point3 = new Point(point1.X, point2.Y);
    var point4 = new Point(point2.X, point1.Y);

    var lt = new[] { point1, point2, point3, point4 }.MinBy(item => item.X + item.Y);
    var rb = new[] { point1, point2, point3, point4 }.MaxBy(item => item.X + item.Y);

    var hasIntersection = false;
    Parallel.ForEach(sides, side =>
    {
        var isLineInRectangle = side.IsLineInRectangle2(lt, rb);

        if (!hasIntersection && isLineInRectangle)
        {
            hasIntersection = true;
        }
    });

    return hasIntersection;
}

public record struct Point(long X, long Y);

public class Line
{
    public Point Start { get; init; }
    public Point End { get; init; }
    public bool IsVertical { get; init; }

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
        IsVertical = start.X == end.X;
    }

    public bool IsLineInRectangle2(Point lt, Point rb)
    {
        if (IsPointInRectangle(lt, rb, Start.X, Start.Y) || IsPointInRectangle(lt, rb, End.X, End.Y))
        {
            return true;
        }
        if (IsVertical)
        {
            var start =  Math.Min(Start.Y, End.Y);
            var end = Math.Max(Start.Y, End.Y);
            return lt.X < Start.X && rb.X > Start.X && start <= lt.Y &&  end >= rb.Y;
        }
        else
        {
            var start =  Math.Min(Start.X, End.X);
            var end = Math.Max(Start.X, End.X);
            return lt.Y < Start.Y && rb.Y > Start.Y && start <= lt.X &&  end >= rb.X;
        }

        return false;
    }

    private bool IsPointInRectangle(Point lt, Point rb, long x, long y)
    {
        if (x > lt.X && x < rb.X && y > lt.Y && y < rb.Y)
        {
            return true;
        }

        return false;
    }
}